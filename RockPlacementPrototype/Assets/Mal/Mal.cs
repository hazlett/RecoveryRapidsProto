﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace OhioState.CanyonAdventure
{
    [XmlRoot("Mal")]
    public class Mal
    {
        [XmlAttribute("id")]
        public int id;

        [XmlArray("prompts")]
        [XmlArrayItem("prompt")]
        public List<Prompt> promptList;

        public Mal() 
        {
            this.promptList = new List<Prompt>();
        }

        [XmlIgnore]
        public bool Asked { get; set; }

        public Prompt GetPrompt(int value)
        {
            try
            {
                return this.promptList.ElementAt(value);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void AddPrompt(Prompt prompt)
        {
            this.promptList.Add(prompt);
        }
    }
}
