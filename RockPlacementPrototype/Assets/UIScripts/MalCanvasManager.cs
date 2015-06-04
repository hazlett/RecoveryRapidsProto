using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using OhioState.CanyonAdventure;

public class MalCanvasManager : MonoBehaviour {

    private Button[] button = new Button[5];
    private Text[] text = new Text[5];
    public ButtonComponent[] buttonComponents = new ButtonComponent[5];
    private RectTransform[] rect = new RectTransform[5];
    private Text questionText, responseText;
    private GameObject responseWindow;
    private GameObject movieWindow, buttons;
    private bool moviePlaying;
    private Vector3 buttonsResetPosition;


	void Awake () {
        movieWindow = GameObject.Find("MovieWindow");
        movieWindow.SetActive(false);
        buttons = GameObject.Find("Buttons");
        for (int i = 0; i < button.Length; i++)
        {
            button[i] = GameObject.Find("Button" + (i + 1).ToString()).GetComponent<Button>();
            text[i] = button[i].transform.GetChild(0).GetComponent<Text>();
            rect[i] = button[i].gameObject.GetComponent<RectTransform>();
            int captured = i;
            button[i].onClick.AddListener(() => ExecuteButton(captured));
            text[i].text = ""; 
        }

        questionText = GameObject.Find("QuestionText").GetComponent<Text>();

        responseWindow = GameObject.Find("ResponseWindow");
        responseText = responseWindow.transform.GetChild(0).GetComponent<Text>();

        responseWindow.SetActive(false);
	}
  
    internal void ActivateMovie(string file)
    {
        if (!movieWindow.activeSelf)
        {
            AdjustButtons();
            movieWindow.SetActive(true);
            MovieStream.Instance.LoadVideoClip(file);
            moviePlaying = true;
        }
    }
    internal void DeactivateMovie()
    {
        if (movieWindow.activeSelf)
        {
            ResetButtons();
            MovieStream.Instance.UnloadVideo();
            movieWindow.SetActive(false);
            moviePlaying = false;
        }
    }
    private void AdjustButtons()
    {
        buttonsResetPosition = buttons.transform.position;
        buttons.transform.localPosition = new Vector3(0, -330, 0);
    }
    private void ResetButtons()
    {
        buttons.transform.position = buttonsResetPosition;
    }

    internal void ExecuteButton(int i)
    {
       // GetComponent<AudioSource>().Stop();
        MalSetManager.Instance.ButtonResponse(buttonComponents[i]);
    }

    internal void SetButtonProperties(int buttonNumber, ButtonComponent bc)
    {

        buttonComponents[buttonNumber] = bc;
        text[buttonNumber].text = bc.Text;
    }

    internal void SetQuestionText(string question)
    {
        questionText.text = question;
    }

    internal void SetResponseWindow(string response)
    {
        responseWindow.SetActive(true);
        responseText.text = response;
    }

    internal void ClearButtons()
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].text == "")
            {
                button[i].gameObject.SetActive(false);
            }
        }
    }

    internal void ResetCanvas()
    {
        for (int i = 0; i < text.Length; i++)
        {
            button[i].gameObject.SetActive(true);
            text[i].text = "";
        }

        responseWindow.SetActive(false);
    }
}
