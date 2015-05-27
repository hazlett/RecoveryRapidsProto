using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MalCanvasManager : MonoBehaviour {

    private Button[] button = new Button[5];
    private Text[] text = new Text[5];
    private RectTransform[] rect = new RectTransform[5];
    private Text questionText, responseText;
    private GameObject responseWindow;

	void Start () {

        for (int i = 0; i < button.Length; i++)
        {
            button[i] = GameObject.Find("Button" + (i + 1).ToString()).GetComponent<Button>();
            text[i] = button[i].transform.GetChild(0).GetComponent<Text>();
            rect[i] = button[i].gameObject.GetComponent<RectTransform>();

            //button[i].onClick().addListener(() => someMethod());

            text[i].text = ""; 
        }

        questionText = GameObject.Find("QuestionText").GetComponent<Text>();

        responseWindow = GameObject.Find("ResponseWindow");
        responseText = responseWindow.transform.GetChild(0).GetComponent<Text>();

        responseWindow.SetActive(false);
	}
	
	void Update () {
	
	}

    internal void SetButtonProperties(int buttonNumber, string newText)
    {
        text[buttonNumber].text = newText;
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
