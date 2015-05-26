using System;
using System.Xml.Serialization;
using UnityEngine;

namespace OhioState.CanyonAdventure
{
    [XmlRoot("button")]
    public class ButtonComponent
    {
        
        public float CollisionHeight;
        public float CollisionWidth;
        // private ButtonOrientation orientation;

        public ButtonComponent()
        {
           
        }

        /// <summary>
        /// This Id of the button component, should be assigned when added by the prompt
        /// </summary>
        [XmlAttribute("id")]
        public int Id { get; set; }

        /// <summary>
        /// The id of the video that will be played
        /// </summary>
        [XmlAttribute("videofollowupid")]
        public int VideoFollowUpId { get; set; }

        /// <summary>
        /// The texture of the button
        /// </summary>
        [XmlIgnore]
        public Texture2D Texture
        {
            get
            {
				return null;
            }
        }

        public float SetTextureHight
        {
            set
            {
                CollisionHeight = value;
            }
        }


        public float SetTextureWidth
        {
            set
            {
                CollisionWidth = value;
            }
        }

        /// <summary>
        /// This is the text that will be displayed on the button component
        /// </summary>
        [XmlAttribute("text")]
        public String Text { get; set; }

        /// <summary>
        /// This is the mal id that will be the follow up if this button has
        /// been pressed
        /// </summary>
        [XmlAttribute("followup")]
        public int FollowUp { get; set; }

        /// <summary>
        /// This the rating score for when the button component is pressed
        /// </summary>
        [XmlAttribute("rating")]
        public float Rating { get; set; }

        /// <summary>
        /// This is a flag to determine if this mal should be asked in future
        /// iterations of the MalSet
        /// </summary>
        [XmlAttribute("eliminatemal")]
        public bool EliminateMal { get; set; }

        [XmlAttribute("soundfile")]
        public string SoundFile { get; set; }

        [XmlAttribute("prompt")]
        public string TextPrompt { get; set; }

        public void SetOrientation(Prompt.ButtonOrientation orientation)
        {
            if (orientation == Prompt.ButtonOrientation.HORIZONTAL)
            {
                
            }

            else
            {
        
            }
        }

        /// <summary>
        /// Setup the orientation of the button component
        /// </summary>
        /* [XmlAttribute("orientation")]
        public ButtonOrientation Orientation
        {
            set
            {
                this.orientation = value;

                if (this.orientation == ButtonOrientation.HORTIZONTAL)
                {
                    this.defaultTexture = defaultHortizontalTexture;
                    this.onOverTexture = onOverHortizontalTexture;
                }

                else if (this.orientation == ButtonOrientation.VERTICAL)
                {
                    this.defaultTexture = defaultVerticalTexture;
                    this.onOverTexture = onOverVerticalTexture;
                }
            }

            get
            {
                return this.orientation;
            }
        } */

        /// <summary>
        /// Check the if there is a collision with the button, if there is
        /// returns true, false otherwise
        /// </summary>
        /// <param name="cursor">The cursor</param>
        /// <returns>Returns if there is a collision detected with the cursor</returns>
        public bool CheckCollision(Cursor cursor, Rect bounds)
        {
			return false;
        }
    }
}
