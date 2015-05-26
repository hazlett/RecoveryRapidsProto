using UnityEngine;
using System.Collections.Generic;

namespace OhioState.CanyonAdventure
{
    public class PromptLayout
    {
        public Rect Region1 { get; set; }
        public Rect Region2 { get; set; }
        public Rect Region3 { get; set; }
        public Rect textInRegion1 { get; set; }
        public Rect videoInRegion2 { get; set; }
        public List<Rect> buttonsInRegion3 { get; set; }

        public PromptLayout()
        {
            buttonsInRegion3 = new List<Rect>();
        }
    }
}
