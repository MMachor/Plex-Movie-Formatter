using System;
using DevExpress.Skins;

namespace Plex_Movie_Formatter
{
    public partial class Options : DevExpress.XtraEditors.XtraForm
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            SkinContainerCollection skins = SkinManager.Default.Skins;
            for (int i = 0; i < skins.Count; i++)
                comboBoxEdit1.Properties.Items.Add(skins[i].SkinName);
        }
    }
}