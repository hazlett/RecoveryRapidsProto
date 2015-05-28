using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MovieStream : MonoBehaviour
{  
	MovieTexture movieTexture;
	void Start()
    {
        Debug.Log("MovieStream: Start");
    }
	void OnEnable ()   {
        Debug.Log("MovieStream: OnEnable");
		WWW www = new WWW ("file:///" + @"C:\Users\hazlett\Documents\GitHub\RecoveryRapidsProto\RockPlacementPrototype\TurnOnLight1.ogv");  
		if (www.error != null)
		{
			Debug.Log(www.error);
		}
		movieTexture = (MovieTexture) www.movie;

        GetComponent<RawImage>().texture = movieTexture;

		GetComponent<AudioSource>().clip = movieTexture.audioClip;

        StartClip();
	}

    void OnDisable()
    {
        Debug.Log("MovieStream: OnDisable");
        movieTexture.Stop();
        GetComponent<AudioSource>().Stop();
	}  
    void StartClip()
    {
        Debug.Log("MovieStream: StartClip");
        movieTexture.loop = true;
        movieTexture.wrapMode = TextureWrapMode.Repeat;
        
        GetComponent<AudioSource>().loop = true;
        movieTexture.Play();
        GetComponent<AudioSource>().Play();
    }
}