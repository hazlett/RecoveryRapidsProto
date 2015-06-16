using UnityEngine;
using System.Collections;
using System;

public class MalButton : MonoBehaviour {

    private bool cursorHovering, transition;
    private float timer, maxTime;
    private MalCanvasManager canvasManager;
    private RectTransform rect;
    private ButtonStruct button;
    public Texture2D MalSelectingTexture;
    private static float cursorSize = 16.0f;
    private static float selectionLength = 3.0f;
    void Start()
    {        
        Initialize();
    }
    private void Initialize()
    {
        transition = false;
        rect = GetComponent<RectTransform>();
        canvasManager = GameObject.Find("MalCanvasManager").GetComponent<MalCanvasManager>();
        timer = 0;
        cursorHovering = false;
        maxTime = 3.0f;
    }
    void Update()
    {
        if ((cursorHovering) && (!MalSetManager.Instance.ResponseMode))
        {
            if (CheckCursorPosition())
            {
                timer += Time.unscaledDeltaTime;
                if (timer >= maxTime)
                {
                    OnCursorExit();
                    transition = true;
                    try
                    {
                        button.ExecuteButton();
                    }
                    catch(Exception e)
                    {
                        Debug.Log("ExecuteButton error in MalButton:" + e.Message);
                    }
                }
            }
            else
            {
                cursorHovering = false;
            }
        }
        else if (transition)
        {
            transition = false;
            if (CheckCursorPosition())
            {
                OnCursorEnter();
            }
        }
        else
        {
            cursorHovering = false;
        }
    }
    void OnGUI()
    {
        if (cursorHovering)
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, selectionLength * cursorSize * ((timer % maxTime) / maxTime), cursorSize), MalSelectingTexture);
        }
    }
    public void OnCursorEnter()
    {
        timer = 0;
        cursorHovering = true;
    }
    public void OnCursorExit()
    {
        cursorHovering = false;
        timer = 0;
    }

    /// <summary>
    /// check button bounds and mouse position to make sure cursor is within the button
    /// return true if cursor is within button. false otherwise
    /// </summary>
    private bool CheckCursorPosition()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, null);
    }
    internal void SetButton(ButtonStruct bs)
    {
        this.button = bs;
    }
}
