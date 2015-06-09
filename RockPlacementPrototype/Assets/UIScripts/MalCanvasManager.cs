using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using OhioState.CanyonAdventure;
using System.Collections.Generic;

public class MalCanvasManager : MonoBehaviour {

    private List<ButtonStruct> buttons;
    public Text questionText, responseText;
    public GameObject responseWindow;
    public GameObject movieWindow;
    private bool moviePlaying;
    private Vector3 buttonsResetPosition;
    private RectTransform responseWindowRect;


	void Awake () {
        movieWindow = MovieStream.Instance.gameObject;
        buttons = new List<ButtonStruct>();
       

	}
    void OnEnable()
    {
        movieWindow.SetActive(false);
        responseWindow.SetActive(false);
    }
    internal void ActivateMovie(string file)
    {
        if (!movieWindow.activeSelf)
        {
            movieWindow.SetActive(true);
            MovieStream.Instance.LoadVideoClip(file);
            moviePlaying = true;
        }
    }
    internal void DeactivateMovie()
    {
        try
        {
            if (movieWindow.activeSelf)
            {
                MovieStream.Instance.UnloadVideo();
                movieWindow.SetActive(false);
                moviePlaying = false;
            }
        }
        catch { }
    }

    internal void SetButtonProperties(List<ButtonComponent> buttonComponenets)
    {
        buttons = MalButtonsManager.Instance.SetButtons(buttonComponenets);
        
    }

    internal void SetQuestionText(string question)
    {
        questionText.text = question;
    }

    internal void SetResponseWindow(string response)
    {
        ResetCanvas();

        responseWindow.SetActive(true);

        responseText.text = response;
    }


    internal void ResetCanvas()
    {
        questionText.text = "";
        try { MalButtonsManager.Instance.ClearButtons(); }
        catch { }
        responseWindow.SetActive(false);
    }
}
