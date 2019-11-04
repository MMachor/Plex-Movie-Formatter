using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Plex_Movie_Formatter
{
    public partial class Plex_Movie_Formatter : DevExpress.XtraEditors.XtraForm
    {
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
            getMetadata.ffmpegHandler(fileNames);
        }
    }
}