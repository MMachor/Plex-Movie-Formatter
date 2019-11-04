using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Plex_Movie_Formatter
{
    class File_Name_Fix
    {
        private readonly String findNameAndYear = @"([A-Z,a-z,0-9,-]{1,}\.){1,}\d{4}\.";

        private Dictionary<String, Dictionary<String, String>> metadataFound = new Dictionary<String, Dictionary<String, String>>();

        public void NameHandler(Dictionary<String, Dictionary<String, String>> metadataFound)
        {
            this.metadataFound = metadataFound;
            RenameFiles();
        }

        private void RenameFiles()
        {
            String movieName, originalName, path;

            foreach (KeyValuePair<String, Dictionary<String, String>> movie in metadataFound)
            {
                FileInfo movieFile = new FileInfo(movie.Key);

                path = movieFile.DirectoryName;
                originalName = movieFile.Name;
                movieName = movieFile.Name;

                Regex fileNameYearExpression = new Regex(findNameAndYear);

                MatchCollection nameAndYearMatch = fileNameYearExpression.Matches(movieName);

                String newName = nameAndYearMatch[0].ToString().Replace(".", " ").Trim().Insert(nameAndYearMatch[0].Length - 5, "(").Insert(nameAndYearMatch[0].Length, ")");

                metadataFound[movie.Key].Add("Path", path);
                metadataFound[movie.Key].Add("Original Name", originalName);
                metadataFound[movie.Key].Add("New Name", newName + movieFile.Extension);
            }
        }
    }
}
