using System;
using System.IO;

namespace OhioState.CanyonAdventure
{
    /// <summary>
    /// Logs entrys for when a user uses the Mal
    /// </summary>
    class MalSetLogger
    {
        private static MalSetLogger instance;
        private StreamWriter streamWriter;
        //private string filename = "\\mal.log";

        public static MalSetLogger Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new MalSetLogger();
                }
                return instance;
            }
        }

        /// <summary>
        /// Opens the MalLogger
        /// </summary>
        /// <param name="filename">The filename to write the log file too</param>
        public void Open()
        {
            if (this.streamWriter != null)
            {
                throw new InvalidOperationException("Logger File Open");
            }
            
            string userPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = userPath + "\\Mal.Log";

            this.streamWriter = new StreamWriter(filename, true);
            this.streamWriter.AutoFlush = true;
         }

        /// <summary>
        /// Closes the MalLogger
        /// </summary>
        public void Close()
        {
            if (this.streamWriter != null)
            {
                this.streamWriter.WriteLine('\n');
                this.streamWriter.Close();
                this.streamWriter.Dispose();
                this.streamWriter = null;
            }
        }

        /// <summary>
        /// Writes an entry to the MalLogger
        /// </summary>
        /// <param name="entry">The string represention of the entry</param>
        public void CreateEntry(string entry)
        {
            if (this.streamWriter == null)
            {
                throw new InvalidOperationException("Logger File Not Open");
            }

            this.streamWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("ddMMyyyy hh:mm:ss"), entry);
        }
    }
}
