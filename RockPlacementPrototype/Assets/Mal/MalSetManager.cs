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
    private int malAsk, malsAsked;
    private bool finalStatement;
	private bool responseMode;
    internal bool ResponseMode { get { return responseMode; } }
	private string responseText;
	private float responseTimer, responseTime;
	private int videoID;
	private bool videoStarted;
    private bool malIsSet;
    private bool initialized;

    void Awake()
    {
        Debug.Log("MalSetManager Awake");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        Debug.Log("MalSetManager Start");
    }

    void OnEnable()
    {
        if (!initialized)
            return;
        Debug.Log("MalSetManager OnEnable");
        malIsSet = false;
        MalSetLogger.Instance.Open();
        MalSetLogger.Instance.CreateEntry("-- New Session Started --");
        responseMode = false;
        videoStarted = false;
        responseTimer = 0.0f;
        responseTime = 3.0f;
        serializer = new MalSetSerializer();
        malSet = serializer.Read("MalQuestions.xml");
        try
        {
            //open config file and read MalAsk
            malAsk = 3;
        }
        catch
        {
            malAsk = 3;
        }
        SetMal();
        malsAsked = 0;
        Time.timeScale = 0.0f;
        malIsSet = true;
    }
    void OnDisable()
    {
        Debug.Log("MalSetManager OnDisable");
        malIsSet = false;
        Time.timeScale = 1.0f;
        initialized = true;
        //Reset Everything
    }
	void Update ()
	{
        if (!malIsSet) return;
        // Ordering of if statements very important

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
        Debug.Log("SetMal: " + malsAsked);
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

		currentMal = malSet.GetMal ();
		currentMal.Asked = true;
        finalStatement = false;
        MalSetLogger.Instance.CreateEntry ("");
        SetCanvas();
        malsAsked++;
	}
    void OnGUI()
    {
        GUILayout.Label("ASKED: " + malsAsked);
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

        canvasManager.SetQuestionText(currentPrompt.Question);
        SetAudio(currentPrompt.SoundFile);

        MalButtonsManager.Instance.ClearButtons();
        canvasManager.SetButtonProperties(currentPrompt.buttonComponents);
              
    }
    internal void VideoFailed()
    {
        videoStarted = false;
        SetMal();
    }
    internal void ButtonResponse(ButtonComponent bc)
    {
        if ((bc.TextPrompt != "") && (bc.TextPrompt != null))
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
        MalSetLogger.Instance.CreateEntry(String.Format("{0}: {1}", currentPrompt.Question, bc.Text));
    }

}
