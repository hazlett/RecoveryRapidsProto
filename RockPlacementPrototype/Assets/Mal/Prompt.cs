using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace OhioState.CanyonAdventure
{
    [XmlRoot("Prompt")]
    public class Prompt
    {
        public enum ButtonOrientation
        {
            HORIZONTAL,
            VERTICAL
        };


        [XmlArray("buttons")]
        [XmlArrayItem("button")]
        public List<ButtonComponent> buttonComponents;

        [XmlArray("videos")]
        [XmlArrayItem("video")]
        public List<VideoComponent> videoComponents;

        /// <summary>
        /// Initializes a newly created prompt
        /// </summary>
        public Prompt()
        {
            this.buttonComponents = new List<ButtonComponent>();
            this.videoComponents = new List<VideoComponent>();
            this.Question = "";
        }

        /// <summary>
        /// This is the id of the prompt
        /// </summary>
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("questions")]
        public string Question { get; set; }

        [XmlElement("soundfile")]
        public string SoundFile { get; set; }

        public bool SoundPlayed { get; set; }

        [XmlElement("orientation")]
        public ButtonOrientation Orientation { get; set; }

        public string GetButtonPrompt(int value)
        {
            return this.buttonComponents.ElementAt(value).TextPrompt;
        }
        public string GetSoundFile(int value)
        {
            return this.buttonComponents.ElementAt(value).SoundFile;
        }

        /// <summary>
        /// This is the id of the prompt that is the followup of the button
        /// that is pressed
        /// </summary>
        /// <param name="value">The id of the button component</param>
        /// <returns>Returns the id of the prompt that is next to ask in the Mal</returns>
        public int GetFollowUp(int value)
        {
            return this.buttonComponents.ElementAt(value).FollowUp;
        }

        /// <summary>
        /// This is the string representation of the answer that is on a button
        /// </summary>
        /// <param name="value">The id of the button component</param>
        /// <returns>Returns the string representation of the button that
        /// is pressed</returns>
        public string GetAnswer(int value)
        {
            return this.buttonComponents.ElementAt(value).Text;
        }

        /// <summary>
        /// Gets the ranking of the button
        /// </summary>
        /// <param name="value">The id of the button component</param>
        /// <returns>Returns the ranking of the button component with the given
        /// id</returns>
        public float GetRanking(int value)
        {
            return this.buttonComponents.ElementAt(value).Rating;
        }

        /// <summary>
        /// Returns the number of buttons on the prompt
        /// </summary>
        /// <returns>Returns an integer value of the number of buttons</returns>
        public int GetNumberOfButtons()
        {
            return buttonComponents.Count();
        }

        /// <summary>
        /// Adds a button component to the prompt
        /// </summary>
        /// <param name="component">The button component that is to be
        /// added to the prompt</param>
        public void AddButtonComponent(ButtonComponent component)
        {
            // set the id to the number of components, this is an
            // identifier to where it is in the list
            component.Id = buttonComponents.Count();

            // add it to the button components list
            this.buttonComponents.Add(component);
        }

        /// <summary>
        /// The enumerator for the button components
        /// </summary>
        /// <returns>The enumerator of buttons</returns>
        public IEnumerator<ButtonComponent> GetButtonComponents()
        {
            return this.buttonComponents.GetEnumerator();
        }

        /// <summary>
        /// Adds a video to the prompt
        /// </summary>
        /// <param name="component">The video component that will be added
        /// to the prompt</param>
        //public void AddVideoComponent(VideoComponent component)
        //{
        //    // set the id to the number of components, this is an
        //    // identifier to where it is in the list
        //    component.Id = videoComponents.Count();

        //    // add it to the video components list
        //    this.videoComponents.Add(component);
        //}

        /// <summary>
        /// The enumerator for the video components
        /// </summary>
        /// <returns>Returns the enumerator video</returns>
        //public IEnumerator<VideoComponent> GetVideoComponents()
        //{
        //    return this.videoComponents.GetEnumerator();
        //}

        /// <summary>
        /// Gets the first video in the prompt
        /// </summary>
        /// <returns>Returns a video</returns>
        //public VideoComponent GetVideo()
        //{
        //    return this.videoComponents.First();
        //}

        public VideoComponent GetVideo(int id)
        {
            return this.videoComponents.ElementAt(id);
        }

        /// <summary>
        /// Return if the prompt has a video, false otherwise
        /// </summary>
        /// <returns>Returns true if there is a video in the prompt, false otherwise</returns>
        public bool HasVideo()
        {
            return (videoComponents.Count > 0);
        }

        /// <summary>
        /// Gets the Id of the video that will be follow up with
        /// </summary>
        /// <param name="id">The id of the button component</param>
        /// <returns>Returns the id of the video that will be used at the
        /// next prompt</returns>
        public int GetVideoFollowUpId(int id)
        {
            return this.buttonComponents.ElementAt(id).VideoFollowUpId;
        }

        /// <summary>
        /// Get the true or false value of the elimate mal flag of the given
        /// button id
        /// </summary>
        /// <param name="id">The id of the button</param>
        /// <returns>Returns the elimate mal flag on the button component id</returns>
        public bool GetEliminateMal(int id)
        {
            return this.buttonComponents.ElementAt(id).EliminateMal;
        }

        /// <summary>
        /// Loads the video with the given id to the prompt
        /// </summary>
        /// <param name="id">The id to the video</param>
        //public void LoadVideo(int id)
        //{
        //    if (videoComponents.Count() > 0)
        //    {
        //        videoComponents.ElementAt(id).LoadVideo();
        //    }

        //    // if there are no videos in the prompt
        //    else
        //    {
        //        // then this is an error
        //        Console.WriteLine("Error: video follow up was specified, but no" +
        //            "videos were loaded to the prompt");
        //    }
        //}

 

        //public void LoadVideo()
        //{
        //    if (videoComponents.Count() > 0)
        //    {
        //        videoComponents.ElementAt(0).LoadVideo();
        //    }

        //    else
        //    {
        //        Console.WriteLine("error: video follow up was specified, but no" +
        //            "videos were loaded to the prompt");
        //    }
        //}

        //public void UnloadVideo()
        //{
        //    if (videoComponents.Count() > 0)
        //    {
        //        videoComponents.ElementAt(0).UnloadVideo();
        //    }
        //}

        public void Initialize()
        {
            foreach (ButtonComponent component in buttonComponents)
            {
                if (this.HasVideo())
                {
                    component.SetOrientation(ButtonOrientation.HORIZONTAL);
                }
                else
                {
                    if (this.Orientation == ButtonOrientation.HORIZONTAL)
                    {
                        component.SetOrientation(ButtonOrientation.HORIZONTAL);
                    }
                    else if (this.Orientation == ButtonOrientation.VERTICAL)
                    {
                        component.SetOrientation(ButtonOrientation.VERTICAL);
                    }
                }
            }
        }

        /// <summary>
        /// Checks the collision with all of the responces in the possible
        /// responces
        /// </summary>
        /// <returns>returns the element that has detected a collision, returns -1
        /// otherwise</returns>
        public int CheckCollision(Cursor cursor, PromptLayout promptLayout)
        {
            int returnValue = -1;

            // iterate through the button components list
            int index = 0;
            foreach (ButtonComponent component in buttonComponents)
            {
                if (component.CheckCollision(cursor, promptLayout.buttonsInRegion3[ index ]))
                {
                    returnValue = component.Id;
                }
                index++;
            }
            return returnValue;
        }
    }
}
