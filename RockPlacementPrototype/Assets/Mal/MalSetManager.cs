using UnityEngine;
using System.Collections;
using OhioState.CanyonAdventure;
using System;

public class MalSetManager : MonoBehaviour {
	private MalSet malSet;
	private MalSetSerializer serializer;
	public Mal currentMal;
	public int currentPromptID;
	public Prompt currentPrompt;
	public MovieTexture movie;
    public MalCanvasManager canvasManager;

	private bool responseMode;
	private string responseText;
	private float responseTimer, responseTime;
	private int videoID;
	private bool videoStarted;

	void Start () {
		MalSetLogger.Instance.Open ();
		MalSetLogger.Instance.CreateEntry ("-- New Session Started --");
		responseMode = false;
		videoStarted = false;
		responseTimer = 0.0f;
		responseTime = 3.0f;
		serializer = new MalSetSerializer ();
		malSet = serializer.Read ("MalQuestions.xml");
		foreach (Mal mal in malSet.malList) {
			Debug.Log(mal.id);
		}
		SetMal ();
	}

	void Update ()
	{
		if (responseMode) {
			responseTimer += Time.deltaTime;
		}
		if (responseTimer > responseTime) {
			responseMode = false;
			responseTimer = 0;
            SetCanvas();
		}

	}
	private void SetMal()
	{
		if (malSet.AllAsked()) {
			Debug.Log("All asked");
		}

		currentPromptID = 0;
		videoID = -1;

		currentMal = malSet.GetMal ();
		currentMal.Asked = true;
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
            canvasManager.SetButtonProperties(i, currentPrompt.buttonComponents[i].Text);
        }

        canvasManager.ClearButtons();
    }

	void OnGUI()
	{
		if (responseMode) {
			GUILayout.Label(responseText);
			return;
		}
		if (currentPromptID == -1) {
			currentMal.Asked = true;
			SetMal();
			return;
		}
		if (movie != null) {
			if (movie.isPlaying)
			{
				GUILayout.Label("MOVIE IS PLAYING");
				return;
			}
		}
		currentPrompt = currentMal.GetPrompt (currentPromptID);
		GUILayout.Label(currentPrompt.Question);
		if (currentPrompt.HasVideo()) {
			GUILayout.Label("HAS VIDEO");
			GUILayout.Label(currentPrompt.GetVideo(videoID).Filename);
			if (!videoStarted)
			{
				videoStarted = true;
				StartCoroutine(LoadVideo(currentPrompt.GetVideo(videoID).Filename));
			}
		}
		if (currentPrompt.buttonComponents.Count == 0) {
			responseMode = true;
			responseText = currentPrompt.Question;
			SetMal();
		} 
		else {
			foreach (ButtonComponent bc in currentPrompt.buttonComponents) {
				if (GUILayout.Button (bc.Text)) {
                    ButtonResponse(bc);
				}
			}
		}
	}

    private void ButtonResponse(ButtonComponent bc)
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
