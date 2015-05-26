using UnityEngine;
using System.Collections;

public class RailManager : MonoBehaviour {
	public int NumberOfRails;
	// Use this for initialization
	void Start () {
		if (NumberOfRails % 2 == 0) {
			NumberOfRails = 3;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
