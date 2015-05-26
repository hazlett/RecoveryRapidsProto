using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MovieStream : MonoBehaviour
{  
	MovieTexture movieTexture;
	
	void Start ()   {
		WWW www = new WWW ("file:///" + @"C:\Users\hazlett\Documents\GitHub\RecoveryRapidsProto\RockPlacementPrototype\TurnOnLight1.ogv");  
		if (www.error != null)
		{
			Debug.Log(www.error);
		}
		movieTexture = (MovieTexture) www.movie;
		GetComponent<GUITexture>().texture = movieTexture;
		GetComponent<AudioSource>().clip = movieTexture.audioClip;
	}
	
	void Update ()  {
		if (movieTexture.isReadyToPlay && !movieTexture.isPlaying)
		{      
			movieTexture.Play();
			GetComponent<AudioSource>().Play();
		}          
	}  
}