using UnityEngine;
using System.Collections;
using OhioState.CanyonAdventure;

public class MalSetManager : MonoBehaviour {
	private MalSet malSet;
	private MalSetSerializer serializer;
	public Mal currentMal;
	public int currentPromptID;
	public Prompt currentPrompt;

	private bool responseMode;
	private string responseText;
	private float responseTimer, responseTime;
	private int videoID;

	void Start () {
		responseMode = false;
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
		Debug.Log("Current Mal: " + currentMal.id + " : " + currentMal.Asked);

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
		currentPrompt = currentMal.GetPrompt (currentPromptID);
		GUILayout.Label(currentPrompt.Question);
		if (currentPrompt.HasVideo()) {
			GUILayout.Label("HAS VIDEO");
			GUILayout.Label(currentPrompt.GetVideo(videoID).Filename);
		}
		if (currentPrompt.buttonComponents.Count == 0) {
			responseMode = true;
			responseText = currentPrompt.Question;
			SetMal();
		} 
		else {
			foreach (ButtonComponent bc in currentPrompt.buttonComponents) {
				if (GUILayout.Button (bc.Text)) {
					if ((bc.TextPrompt != "") && (bc.TextPrompt != null)) {
						responseMode = true;
						responseText = bc.TextPrompt;
					}
					currentPromptID = bc.FollowUp;
					videoID = bc.VideoFollowUpId;
				
				}
			}
		}
	}
}
