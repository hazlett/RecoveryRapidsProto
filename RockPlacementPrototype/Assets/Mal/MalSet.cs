using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace OhioState.CanyonAdventure
{
    [XmlRoot("MalSet")]
    public class MalSet
    {
        [XmlArray("Mals")]
        [XmlArrayItem("Mal")]
        public List<Mal> malList;

        public MalSet() 
        {
            this.malList = new List<Mal>();
        }

        /// <summary>
        /// Returns the number of mals in the list
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return malList.Count();
        }

        public Mal GetMal()
        {
            // the mal that will be returned
            int malIndex = 0;
            
            // if all mals have been asked, reset them
            if (this.AllAsked())
            {
                this.Reset();
            }


            // if its sequential
            
           // the mal index is the next mal that has not been asked
           while (this.BeenAsked(malIndex)) {
				malIndex++;
			}

//            // if its random
//            else if (MalSetConfigurer.Instance.ordering == MalSetConfigurer.Ordering.RANDOM)
//            {
//                // pick a random one that has not been asked
//                Random random = new Random();
//                int randomMal = random.Next(this.Count());
//
//                // keep picking till one is found that has not been asked
//                while (this.BeenAsked(randomMal) == true)
//                {
//                    // pick another one
//                    randomMal = random.Next(this.Count());
//                }
//                malIndex = randomMal;
//            }

            return malList.ElementAt(malIndex);
        }

        /// <summary>
        /// This picks a mal from the mal set given the ordering and
        /// settings specified from the loaded configuration file
        /// </summary>
        /// <returns></returns>
        public Mal GetMal(int value)
        {
            return this.malList.ElementAt(value);
        }

        /// <summary>
        /// Adds a mal to the MalSet
        /// </summary>
        /// <param name="mal"></param>
        public void AddMal(Mal mal)
        {
            this.malList.Add(mal);
        }

        /// <summary>
        /// Reset all the Asked flags on the Mals to be asked again
        /// </summary>
        public void Reset()
        { 
            // set all Asked flags to false
            foreach (Mal mal in malList)
            {
                mal.Asked = false;
            }
        }

        /// <summary>
        /// Set the asked flag to true for the given mal
        /// </summary>
        /// <param name="malIndex">The mal to set the asked flag to true</param>
        public void SetAsked(int malIndex)
        {
            this.malList.ElementAt(malIndex).Asked = true;
        }

        /// <summary>
        /// Checks if the mal has already been asked, returns true if it has
        /// and false otherwise
        /// </summary>
        /// <param name="malIndex">The mal index</param>
        /// <returns></returns>
        public bool BeenAsked(int malIndex)
        {
            return this.malList.ElementAt(malIndex).Asked;
        }

        /// <summary>
        /// Checks each Mal and see if they have been asked,
        /// if they have return true, false otherwise
        /// </summary>
        public bool AllAsked()
        {
            foreach (Mal mal in malList)
            {
                if (mal.Asked == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
