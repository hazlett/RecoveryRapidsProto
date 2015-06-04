using UnityEngine;
using System.Collections;
using OhioState.CanyonAdventure;

public class MalButtonsManager : MonoBehaviour {

    private static MalButtonsManager instance;
    internal static MalButtonsManager Instance { get { return instance; } }
    public GameObject ButtonInstance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        SetButtons(null, 0);
    }

    internal void SetButtons(ButtonComponent[] buttons, float size)
    {
        GameObject go = new GameObject();
    }
}
