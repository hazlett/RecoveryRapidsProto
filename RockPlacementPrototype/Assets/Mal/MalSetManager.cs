using UnityEngine;
using System.Collections;
using OhioState.CanyonAdventure;
using System;

public class MalSetManager : MonoBehaviour {
    private static MalSetManager instance;
    public static MalSetManager Instance { get { return instance; } }
    private MalSet malSet;
	private MalSetSerializer serializer;
	public Mal currentMal;
	public int currentPromptID;
	public Prompt currentPrompt;
	public MovieTexture movie;
    public MalCanvasManager canvasManager;
    public AudioSource audioSource;
    
    public int malAsk, malsAsked;
    public bool responseOn;
    private bool finalStatement;
	private bool responseMode;
	private string responseText;
	private float responseTimer, responseTime;
	private int videoID;
	private bool videoStarted;
    private bool malIsSet;
    private bool initialized;
    internal bool ResponseMode { get { return responseMode; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            serializer = new MalSetSerializer();
            malSet = serializer.Read("MalQuestions.xml");
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        SetActive(false);
    }
    internal void SetActive(bool set)
    {
        gameObject.SetActive(set);
    }
    internal void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    void OnEnable()
    {
        if (!initialized)
            return;
        responseOn = false;
        malIsSet = false;
        MalSetLogger.Instance.Open();
        MalSetLogger.Instance.CreateEntry("-- New Session Started --");
        responseMode = false;
        videoStarted = false;
        responseTimer = 0.0f;
        responseTime = 3.0f;
        try
        {
            //open config file and read MalAsk
            malAsk = 3;
        }
        catch
        {
            malAsk = 3;
        }
        malsAsked = 0;
        SetMal();
       
        Time.timeScale = 0.0f;
        malIsSet = true;

    }
    void OnDisable()
    {
        malIsSet = false;
        Time.timeScale = 1.0f;
        initialized = true;
        //Reset Everything
    }

	void Update ()
    {
        /* Ordering of if statements very important */

        if (!malIsSet) return;

		if (responseMode) {
			responseTimer += Time.unscaledDeltaTime;
		}
		if ((responseTimer > responseTime) && (!audioSource.isPlaying))
        {
			responseMode = false;
			responseTimer = 0;
            if (finalStatement)
                SetMal();
            else
                SetCanvas();
		}
        if (currentPromptID == -1)
        {
            currentMal.Asked = true;
            SetMal();
            return;
        }
        if ((currentPrompt.buttonComponents.Count == 0) && (!responseMode))
        {
            responseMode = true;
            responseText = currentPrompt.Question;
            finalStatement = true;
        }
        if (currentPrompt.HasVideo() && !videoStarted)
        {
            canvasManager.ActivateMovie(currentPrompt.GetVideo(videoID).Filename);
            videoStarted = true;
        }
	}

	private void SetMal()
    {
        if (malsAsked >= malAsk)
        {
            gameObject.SetActive(false);
            return;
        }
		if (malSet.AllAsked()) {
			Debug.Log("All of malSet asked. Resetting.");
            malSet.Reset();
		}
        canvasManager.DeactivateMovie();
		currentPromptID = 0;
		videoID = -1;

        currentMal = malSet.GetMal(GetRandomMal());
        currentMal.Asked = true;
        finalStatement = false;
        MalSetLogger.Instance.CreateEntry ("");
        SetCanvas();
        malsAsked++;
	}
    private int GetRandomMal()
    {
        System.Random random = new System.Random();
        int index = random.Next(malSet.Count());
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        double timeout = 100;
        
        while (malSet.BeenAsked(index) && (stopwatch.Elapsed.TotalMilliseconds <= timeout))
        {
            index = random.Next(malSet.Count());
        }
        if (stopwatch.Elapsed.TotalMilliseconds >= timeout)
        {
            Debug.Log("Timeout occured: " + stopwatch.Elapsed.TotalMilliseconds);
        }
        return index;
    }

    private void SetAudio(string file)
    {
        StartCoroutine(LoadAudioClip(file + ".wav"));
    }
    private IEnumerator LoadAudioClip(string file)
    {
        WWW www = new WWW("file:///" + @"C:\Users\hazlett\Documents\GitHub\RecoveryRapidsProto\RockPlacementPrototype\MALSounds\" + file);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        audioSource.clip = www.audioClip;
        audioSource.Play();
    }
    private void SetCanvas()
    {
        canvasManager.ResetCanvas();

        currentPrompt = currentMal.GetPrompt(currentPromptID);
        if (currentPrompt != null)
        {
            canvasManager.SetQuestionText(currentPrompt.Question);
            SetAudio(currentPrompt.SoundFile);
            MalButtonsManager.Instance.ClearButtons();
            canvasManager.SetButtonProperties(currentPrompt.buttonComponents);
        }
        else
        {
            MalButtonsManager.Instance.ClearButtons();
        }
              
    }
    internal void VideoFailed()
    {
        videoStarted = false;
        SetMal();
    }
    internal void ButtonResponse(ButtonComponent bc)
    {
        if ((bc.TextPrompt != "") && (bc.TextPrompt != null) && responseOn)
        {
            responseMode = true;
            responseText = bc.TextPrompt;

            canvasManager.SetResponseWindow(responseText);
        }
        currentPromptID = bc.FollowUp;
        videoID = bc.VideoFollowUpId;
        videoStarted = false;

        if (!responseMode)
        {
            SetCanvas();
        }
        else
        {
            SetAudio(bc.SoundFile);
        }
        if (currentPrompt != null)
            MalSetLogger.Instance.CreateEntry(String.Format("{0}: {1}", currentPrompt.Question, bc.Text));
    }

}
