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

    private bool finalStatement;
	private bool responseMode;
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
		if (responseTimer > responseTime) {
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
        if (currentPrompt.HasVideo())
        {
            canvasManager.ActivateMovie();
        }


	}
	private void SetMal()
	{
        Debug.Log("SetMal");
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
		Debug.Log("Current Mal: " + currentMal.id + " : " + currentMal.Asked);

        SetCanvas();

	}

    private void SetCanvas()
    {
        canvasManager.ResetCanvas();

        currentPrompt = currentMal.GetPrompt(currentPromptID);

        canvasManager.SetQuestionText(currentPrompt.Question);
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
        MalSetLogger.Instance.CreateEntry(String.Format("{0}: {1}", currentPrompt.Question, bc.Text));
    }

	private IEnumerator LoadVideo(string fileName)
	{
		WWW www = new WWW ("file:///TurnOnLight1.ogv");
		yield return www;
		movie = www.movie;
		movie.Play ();
		Debug.Log ("Movie Play");
	}
}
