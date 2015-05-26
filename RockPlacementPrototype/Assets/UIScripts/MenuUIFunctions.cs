using UnityEngine;
using System.Collections;

public class MenuUIFunctions : MonoBehaviour {

	public void LoadMAL()
	{
		Application.LoadLevel ("MalTest");
	}

	public void LoadRocks()
	{
		Application.LoadLevel ("TestScene");
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
