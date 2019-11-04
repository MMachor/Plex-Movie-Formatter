using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;

namespace Plex_Movie_Formatter
{
    public partial class Plex_Movie_Formatter : DevExpress.XtraEditors.XtraForm
    {
        private DataTable movieInformation = new DataTable();

        public Plex_Movie_Formatter()
        {
            InitializeComponent();
        }

        private void Plex_Movie_Formatter_Load(object sender, EventArgs e)
        {
            Constants.SetConstants();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Xmas 2008 Blue";
        }

        private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog selectFolder = new FolderBrowserDialog())
            {
                selectFolder.ShowDialog();

                if (selectFolder.SelectedPath != null)
                    buttonEdit1.Text = selectFolder.SelectedPath;
            }
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            Get_Files getFiles = new Get_Files();
            List<String> fileNames = getFiles.FileHandler(buttonEdit1.Text, checkEdit1.Checked);
            ffmpeg getMetadata = new ffmpeg();
            movieInformation = getMetadata.ffmpegHandler(fileNames);
            gridControl1.DataSource = movieInformation;
            gridView1.BestFitColumns();
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            foreach(DataRow movie in movieInformation.Rows)
            {
                List<String> fileNames = new List<String>();
                fileNames.Add(movie["Path"] + "\\" + movie["Original Name"]);
                fileNames.Add(movie["Path"] + "\\" + movie["New Name"]);
 
                handbrakeCLI handbrakeTranscode = new handbrakeCLI();

                ThreadPool.SetMinThreads(1, 1);
                ThreadPool.SetMaxThreads(4, 4);
                ThreadPool.QueueUserWorkItem(new WaitCallback(handbrakeTranscode.TranscodeHandler), fileNames);
            }
        }
    }
}