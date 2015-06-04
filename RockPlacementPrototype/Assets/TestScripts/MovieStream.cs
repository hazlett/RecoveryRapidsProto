using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MovieStream : MonoBehaviour
{
    private static MovieStream instance;
    internal static MovieStream Instance { get { return instance; } }
	private MovieTexture movieTexture;
	void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    internal void LoadVideoClip(string file)
    {
        Debug.Log("Loading: " + file);
        StartCoroutine(LoadVideo(file + ".ogg"));
    }
    internal void UnloadVideo()
    {
        if (movieTexture != null)
        {
            movieTexture.Stop();
            movieTexture = null;
        }
    }
    private IEnumerator LoadVideo(string file)
    {
        file = "TurnOnLight1.ogv";
        Debug.Log("Enable Movie");

        WWW www = new WWW("file:///" + @"C:\Users\hazlett\Documents\GitHub\RecoveryRapidsProto\RockPlacementPrototype\" + file);
        yield return www;
        if (www.error != null)
        {
            MalSetManager.Instance.VideoFailed();
            Debug.Log(www.error);
        }
        movieTexture = (MovieTexture)www.movie;

        GetComponent<RawImage>().texture = movieTexture;

        GetComponent<AudioSource>().clip = movieTexture.audioClip;

        StartClip();
    }
    void OnDisable()
    {
        UnloadVideo();
	}  
    void StartClip()
    {
        movieTexture.loop = true;
        movieTexture.wrapMode = TextureWrapMode.Repeat;
        
        GetComponent<AudioSource>().loop = true;
        movieTexture.Play();
    }
}