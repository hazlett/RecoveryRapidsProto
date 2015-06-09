using UnityEngine;
using System.Collections;
using OhioState.CanyonAdventure;
using System.Collections.Generic;

public class MalButtonsManager : MonoBehaviour {

    private static MalButtonsManager instance;
    internal static MalButtonsManager Instance { get { return instance; } }
    public GameObject ButtonInstance;

    void Awake()
    {
        Debug.Log("MalButtonsManager Awake");
        if (instance == null) instance = this;
        else Destroy(this);
    }
    void Start()
    {
        Debug.Log("MalButtonsManager Start");
    }
    internal List<ButtonStruct> SetButtons(List<ButtonComponent> buttonComponents)
    {
        ClearButtons();
        List<ButtonStruct> buttons = new List<ButtonStruct>();
        float offset = 0;
        foreach (ButtonComponent bc in buttonComponents)
        {
            GameObject go = GameObject.Instantiate<GameObject>(ButtonInstance);
            //go.transform.parent = gameObject.transform;
            go.transform.SetParent(transform, false);
            go.transform.position += new Vector3(offset, 0, 0);
            ButtonStruct bs = new ButtonStruct(go, bc);
            go.GetComponent<MalButton>().SetButton(bs);
            buttons.Add(bs);
            offset += 75;
        }
        return buttons;
    }
    internal void ClearButtons()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
