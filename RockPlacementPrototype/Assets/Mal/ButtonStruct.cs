using UnityEngine;
using System.Collections;
using OhioState.CanyonAdventure;
using UnityEngine.UI;

public class ButtonStruct {

    private Button button;
    private ButtonComponent bc;
    private Text buttonText;
    private RectTransform buttonRect;
    internal ButtonStruct(GameObject button, ButtonComponent bc)
    {
        this.button = button.GetComponent<Button>();
        this.buttonText = button.GetComponentInChildren<Text>();
        this.buttonRect = button.GetComponent<RectTransform>();
        this.bc = bc;
        this.button.onClick.AddListener(() => ExecuteButton());
        this.buttonText.text = bc.Text;
    }
    internal void ExecuteButton()
    {
        MalSetManager.Instance.ButtonResponse(bc);
    }
}
