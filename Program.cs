using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace FileSize0001 
{
    class Program
    {
        // | STAThread is necessary for System.Windows.Clipboard services to work.
        [STAThread]
        static void Main(string[] args)
        {
            new Program().Run();
        }

        // | used for state logging, body commented out on release.
        private void log(in string s) {
            // Console.WriteLine(s);
        }

        // | All the work delegated by main happens here, we log our state while developing.
        // | The logs do nothing on release, which is inefficient, but serve dual purpose as comments
        // |    with some runtime overhead.
        // | Our program consists of four states:
        // |     0. entry 
        // |     1. after obtaining command line arguments
        // |     2. after obtaining size of file
        // |     3. end state(after setting the clipboard to the file size)
        // | If there is a failure at any time, main will return an error code for the state(25 + state number). 
        public void Run() 
        {
            string strCurrentFilePath = null;
            string strFileSize = null;
            
            log("state 0: entry state");
            
            try {
                strCurrentFilePath = Environment.GetCommandLineArgs()[1];
            } catch (Exception) {
                Environment.Exit(25);
            }
            log("state 1: got path(" + strCurrentFilePath + ")");
                        
            try {
                strFileSize = this.GetSizeOfFileAsString(strCurrentFilePath);                
            } catch (Exception) {
                Environment.Exit(26);
            }
            log("state 2: got file size(" + strFileSize + ")");
            
            try {
                System.Windows.Clipboard.SetText(strFileSize);
            } catch (Exception) {
                Environment.Exit(27);
            }
            log("state 3: end state, attempting to set clipboard text to " + strFileSize + " did not end our process.");
        }

        // | This application exclusively uses file size as a string.
        private string GetSizeOfFileAsString(in string strFilePath) {
            try {
                FileInfo currentFileInfo = new FileInfo(strFilePath);
                return currentFileInfo.Length.ToString();
            } catch(Exception) {
                return null;
            }
        }
    }
}
