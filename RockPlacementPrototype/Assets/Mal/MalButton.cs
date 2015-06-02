using UnityEngine;
using System.Collections;

public class MalButton : MonoBehaviour {

    private int buttonID;
    private bool cursorHovering, transition;
    private float timer, maxTime;
    private MalCanvasManager canvasManager;
    private RectTransform rect;

    void Start()
    {        
        Initialize();
    }
    private void Initialize()
    {
        transition = false;
        rect = GetComponent<RectTransform>();
        buttonID = int.Parse(gameObject.name.Replace("Button", "")) - 1;
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
                timer += Time.deltaTime;
                if (timer >= maxTime)
                {
                    OnCursorExit();
                    transition = true;
                    canvasManager.ExecuteButton(buttonID);
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
            GUILayout.Label("BUTTONID: " + buttonID);
            GUILayout.Label("timer: " + timer);
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
}
