using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex_Movie_Formatter
{
    class Create_DataTable
    {
        private Dictionary<String, Dictionary<String, String>> metadataCollected = new Dictionary<String, Dictionary<String, String>>();

        public DataTable DataHandler(Dictionary<String, Dictionary<String, String>> metadataCollected)
        {
            this.metadataCollected = metadataCollected;
            return CreateDatatable();
        }

        private DataTable CreateDatatable()
        {
            DataTable moviesInfo = new DataTable();

            foreach (KeyValuePair<String, Dictionary<String, String>> movie in metadataCollected)
            {
                Dictionary<String, String> movieInformation = movie.Value;

                foreach (String keyColumn in movieInformation.Keys)
                {
                    if (!moviesInfo.Columns.Contains(keyColumn))
                    {
                        if (keyColumn != "Transcode")
                            moviesInfo.Columns.Add(keyColumn).DataType = typeof(String);
                        else
                            moviesInfo.Columns.Add(keyColumn).DataType = typeof(Boolean);
                    }
                }

                moviesInfo.Rows.Add();

                foreach (String keyColumn in movieInformation.Keys)
                    if (keyColumn == "Transcode")
                        moviesInfo.Rows[moviesInfo.Rows.Count - 1][keyColumn] = Convert.ToBoolean(movieInformation[keyColumn]);
                    else
                        moviesInfo.Rows[moviesInfo.Rows.Count - 1][keyColumn] = movieInformation[keyColumn];
            }

            return moviesInfo;
        }
    }
}
