using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Plex_Movie_Formatter
{
    class ffmpeg
    {
        public void ffmpegHandler(List<String> fileNames)
        {
            ScanFiles(fileNames);
        }

        private void ScanFiles(List<String> fileNames)
        {
            Dictionary<String, Dictionary<String, String>> metadataResults = new Dictionary<String, Dictionary<String, String>>();

            foreach (String fileName in fileNames)
            {
                String metadata = RetrieveDetails(fileName);
                metadataResults.Add(fileName, ExtractMetadata(metadata));
            }

            File_Name_Fix fixFileNames = new File_Name_Fix();
            fixFileNames.NameHandler(metadataResults);
        }

        private String RetrieveDetails(string file)
        {
            using (Process ffmpeg = new Process())
            {
                ffmpeg.StartInfo.FileName = "ffmpeg";
                ffmpeg.StartInfo.Arguments = String.Format(" -i \"{0}\" -hide_banner", file);
                ffmpeg.StartInfo.UseShellExecute = false;
                ffmpeg.StartInfo.RedirectStandardError = true;
                ffmpeg.StartInfo.CreateNoWindow = true;
                ffmpeg.Start();

                string stdError = ffmpeg.StandardError.ReadToEnd();
                ffmpeg.WaitForExit();

                return stdError;
            }
        }

        private Dictionary<String, String> ExtractMetadata(String videoMetadata)
        {
            Regex videoInformation = new Regex(@"Video:\ [a-z, A-Z]{4}\ ?|Video:\ [a-z, A-Z]{1,9}[1-9]{1,3}\ ?|\d{3,4}x\d{3,4}|Audio:\ [a-z,A-Z]{1,9}\ \(|\d{1,9}\ Hz|\d{1}\.\d{1}?,|stereo?|[1-9]\d\.[1-9]\d fps|5\.1|Duration: (\d{2}:){2}\d{2}");

            MatchCollection videoInformationMatches = videoInformation.Matches(videoMetadata);

            Dictionary<String, String> results = new Dictionary<String, String>();

            if (videoInformationMatches.Count != 0)
            {
                foreach (Match videoInformationGroup in videoInformationMatches)
                {
                    if (videoInformationGroup.ToString().Contains("Video: ") && !results.ContainsKey("Video Encoding"))
                    {
                        results.Add("Video Encoding", videoInformationGroup.ToString().Replace("Video: ", ""));

                        if (videoInformationGroup.ToString().Replace("Video: ", "").Trim().ToUpper() != "H264" && videoInformationGroup.ToString().Replace("Video: ", "").Trim().ToUpper() != "HEVC")
                            results.Add("Transcode", "true");
                        else
                            results.Add("Transcode", "false");
                    }
                    else if (videoInformationGroup.ToString().Contains("x") && !results.ContainsKey("Video Resolution"))
                        results.Add("Video Resolution", videoInformationGroup.ToString());
                    else if (videoInformationGroup.ToString().Contains("fps") && !results.ContainsKey("Video FPS"))
                        results.Add("Video FPS", videoInformationGroup.ToString().ToUpper());
                    else if (videoInformationGroup.ToString().Contains("(") && !results.ContainsKey("Audio Encoding"))
                        results.Add("Audio Encoding", videoInformationGroup.ToString().Replace("(", "").Replace("Audio: ", "").Trim().ToUpperInvariant());
                    else if (videoInformationGroup.ToString().Contains("Hz") && !results.ContainsKey("Audio Frequency"))
                        results.Add("Audio Frequency", videoInformationGroup.ToString());
                    else if ((videoInformationGroup.ToString().Contains("5.1") || videoInformationGroup.ToString().Contains("stereo")) && !results.ContainsKey("Audio Channels"))
                        results.Add("Audio Channels", videoInformationGroup.ToString().ToUpperInvariant());
                    else if (videoInformationGroup.ToString().Contains("Duration: ") && !results.ContainsKey("Video Duration"))
                        results.Add("Video Duration", videoInformationGroup.ToString().Replace("Duration: ", "").Trim());
                }
            }

            return results;
        }
    }
}
