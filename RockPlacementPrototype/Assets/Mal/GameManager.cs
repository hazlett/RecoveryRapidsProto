using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	void Update () {
	    if (Input.GetKeyUp(KeyCode.Space))
        {
            MalSetManager.Instance.Toggle();
        }
	}
}
