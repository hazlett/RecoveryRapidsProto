using System.Xml.Serialization;
using UnityEngine;

namespace OhioState.CanyonAdventure
{
    [XmlRoot("video")]
    public class VideoComponent
	{
        public VideoComponent()
        {
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

    }
}
