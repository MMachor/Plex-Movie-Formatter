using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace Plex_Movie_Formatter
{
    class handbrakeCLI
    {
        public void TranscodeHandler(Object fileNames)
        {
            List<String> test = new List<String>();

            if (fileNames is IEnumerable)
            {
                test = (List<String>)fileNames;
            }
           
            HandbrakeCLI(test[0], test[1]);
        }

        private void HandbrakeCLI(String oldFileName, String newFileName)
        {
            using (Process handbrake = new Process())
            {
                handbrake.StartInfo.FileName = "HandBrakeCLI";
                handbrake.StartInfo.Arguments = String.Format("-i \"{0}\" -o \"{1}\" --audio-lang-list eng --all-audio  --first-audio --preset \"Very Fast 1080p30\" --optimize --two-pass --turbo --aencoder av_aac --mixdown 5point1 --adither auto --auto-anamorphic ", oldFileName, newFileName);
                handbrake.StartInfo.UseShellExecute = true;
                handbrake.StartInfo.RedirectStandardOutput = false;
                handbrake.StartInfo.RedirectStandardError = false;
                handbrake.StartInfo.CreateNoWindow = false;
                handbrake.Start();

                handbrake.WaitForExit();
            }
        }
    }
}
