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

    private bool finalStatement;
	private bool responseMode;
    internal bool ResponseMode { get { return responseMode; } }
	private string responseText;
	private float responseTimer, responseTime;
	private int videoID;
	private bool videoStarted;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

	void Start () {
		MalSetLogger.Instance.Open ();
		MalSetLogger.Instance.CreateEntry ("-- New Session Started --");
		responseMode = false;
		videoStarted = false;
		responseTimer = 0.0f;
		responseTime = 3.0f;
		serializer = new MalSetSerializer ();
		malSet = serializer.Read ("MalQuestions.xml");

		SetMal ();
	}

	void Update ()
	{
        // Ordering of if statements very important

		if (responseMode) {
			responseTimer += Time.deltaTime;
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
		if (malSet.AllAsked()) {
			Debug.Log("All asked");
		}
        canvasManager.DeactivateMovie();
		currentPromptID = 0;
		videoID = -1;

		currentMal = malSet.GetMal ();
		currentMal.Asked = true;
        finalStatement = false;
        MalSetLogger.Instance.CreateEntry ("");
        SetCanvas();

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
        for (int i = 0; i < currentPrompt.buttonComponents.Count; i++)
        {
            canvasManager.SetButtonProperties(i, currentPrompt.buttonComponents[i]);
        }
        
        canvasManager.ClearButtons();
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
