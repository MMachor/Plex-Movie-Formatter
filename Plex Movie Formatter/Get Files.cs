using System;
using System.IO;
using System.Collections.Generic;

namespace Plex_Movie_Formatter
{
    class Get_Files
    {
        private List<String> fileNames = new List<String>();

        public List<String> FileHandler(String directory, Boolean includeSubDirs)
        {
            GetAllFiles(directory, includeSubDirs);
            return fileNames;
        }

        private void GetAllFiles(String directory, Boolean includeSubDirs)
        {
            foreach (String fileName in Directory.GetFiles(directory))
            {
                FileInfo fileInformation = new FileInfo(fileName);

                if (Constants.fileExtensions.Contains(fileInformation.Extension))
                    fileNames.Add(fileName);
            }

            if(includeSubDirs)
                foreach (String subDirectory in Directory.GetDirectories(directory))
                    GetAllFiles(subDirectory, includeSubDirs);
        }
    }
}
