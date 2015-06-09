using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject MAL;
	// Use this for initialization
	void Start () {
        MAL.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.Space))
        {
            MAL.SetActive(!MAL.activeSelf);
        }
	}
}
