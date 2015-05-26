using System.IO;
using System.Xml.Serialization;

namespace OhioState.CanyonAdventure
{
    /// <summary>
    /// Serializes and deserialzes a MalSet
    /// </summary>
    class MalSetSerializer
    {
        public MalSetSerializer() {}

        /// <summary>
        /// Writes a Xml Serialzied file to mal.xml
        /// </summary>
        /// <param name="malSet">The MalSet that will be serialzed to the mal.xml</param>
        public void Write(MalSet malSet)
        {
            XmlSerializer s = new XmlSerializer(typeof(MalSet));
            using (Stream sm = File.Create("MALXML"))
            {
                s.Serialize(sm, malSet);
            }
        }

        /// <summary>
        /// Reads in the Xml file to be used in the program
        /// </summary>
        /// <returns>Returns the MalSet representation of the deserialized
        /// xml file</returns>
        public MalSet Read(string filename)
        {
            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer ser = new XmlSerializer(typeof(MalSet));
                return (MalSet)ser.Deserialize(f);
            }
        }
    }
}
