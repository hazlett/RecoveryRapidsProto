using UnityEngine;
using System.Collections;

public class MovieTest : MonoBehaviour {

	public Material movFile;
	void Start()
	{
		movFile = Resources.Load<Material> ("Materials/SampleVideo_1080x720_2mb 1");
	}
	void Update () {
		if (Input.GetButtonDown ("Jump")) {

			MovieTexture movie = (MovieTexture)movFile.mainTexture;

			if (movie.isPlaying) {
				movie.Pause();
			}
			else {
				movie.Play();
			}
		}
	}
}
