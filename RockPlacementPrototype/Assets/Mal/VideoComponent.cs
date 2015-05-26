using System.Xml.Serialization;
using UnityEngine;

namespace OhioState.CanyonAdventure
{
    [XmlRoot("video")]
    public class VideoComponent
	{
		private class Video
		{

		}
		private class VideoPlayer
		{
			internal bool IsLooped;
			internal void Play (Video video)
			{

			}
			internal Texture2D GetTexture()
			{
				return null;
			}
		}
        private Video video;
        private VideoPlayer videoPlayer;

        public VideoComponent()
        {
            // this.video = MalSetContentManager.Instance.GetVideo(0);
            this.videoPlayer = new VideoPlayer();
            this.videoPlayer.IsLooped = true;
            this.Filename = "Default Name";
        }

        /// <summary>
        /// This is the filename of the video
        /// </summary>
        [XmlAttribute("filename")]
        public string Filename { get; set; }
        
        /// <summary>
        /// The Id for the video component, should be assigned when it added
        /// to the prompt
        /// </summary>
        [XmlAttribute("id")]
        public int Id { get; set; }

        /// <summary>
        /// The position assigned to the video component, should be set during
        /// the prompt initialization
        /// </summary>
        [XmlIgnore]
        public Vector2 Position { get; set; }

        /// <summary>
        /// The texture assigned to the video component
        /// </summary>
        [XmlIgnore]
        public Texture2D Texture
        {
            get
            {
                // start playing the video when the texture is being used
                this.videoPlayer.Play(video);
                return videoPlayer.GetTexture(); 
            }   
        }

        public void LoadVideo()
        {
           // this.video = MalSetContentManager.Instance.LoadVideo(this.Filename);
        }

        public void UnloadVideo()
        {
            this.video = null;
        }
    }
}
