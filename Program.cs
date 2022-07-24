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
            // | There are no reasonable ways to fail, do nothing on failure, set error code to 25 to indicate it didn't work.
            try {
                new Program().Run();
            } catch (Exception) {
                Environment.Exit(25);
            }
        }

        // | used for state logging, body commented out on release.
        private void log(string s) {
            // Console.WriteLine(s);
        }

        public void Run() 
        {
            log("state 0: entry state");
            string strCurrentFilePath = Environment.GetCommandLineArgs()[1];
            log("state 1: got path(" + strCurrentFilePath + ")");
            string strFileSize = this.GetSizeOfFileAsString(strCurrentFilePath);                
            log("state 2: got file size(" + strFileSize + ")");
            // | the below was added to check if it clears, it does not.
            System.Windows.Clipboard.Clear();
            log("state 2.5: attempting to clear the clipboard did not end our process.");
            System.Windows.Clipboard.SetText(strFileSize);
            log("state 3: attempting to set clipboard text to " + strFileSize + " did not end our process.");
            log("state 4: end state");
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
