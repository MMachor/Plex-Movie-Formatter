using System;
using System.Collections.Generic;

namespace Plex_Movie_Formatter
{
    class Constants
    {
        public static List<String> fileExtensions = new List<String>();

        public static void SetConstants()
        {
            fileExtensions.Add(".mp4");
            fileExtensions.Add(".mkv");
            fileExtensions.Add(".flv");
            fileExtensions.Add(".avi");
            fileExtensions.Add(".mov");
            fileExtensions.Add(".wmv");
            fileExtensions.Add(".m4p");
            fileExtensions.Add(".m4v");
            fileExtensions.Add(".mpg");
            fileExtensions.Add(".mp2");
            fileExtensions.Add(".mpeg");
            fileExtensions.Add(".mpe");
            fileExtensions.Add(".mpv");
            fileExtensions.Add(".m2v");
        }
    }
}
