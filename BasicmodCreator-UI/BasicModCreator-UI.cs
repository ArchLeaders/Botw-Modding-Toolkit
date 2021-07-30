using System.Threading.Tasks;
using System.Windows.Forms;
using static BasicModCreator_UI.UI_Controls;

namespace BasicModCreator_UI
{
    public partial class btnSortFiles : Form
    {
        public btnSortFiles()
        {
            InitializeComponent();
        }

        private async void BasicModCreatorUI_Load(object sender, System.EventArgs e)
        {
            FileList.Enabled = false;
            await Task.Run(() => getFiles(FileList));
            FileList.DataSource = botwFiles;
            FileList.Enabled = true;
        }

        private void txtBoxSearchFile_TextChanged(object sender, System.EventArgs e)
        {
            search(txtBoxSearchFile.Text, FileList);
        }

        private void txtBoxSearchAllTools_TextChanged(object sender, System.EventArgs e)
        {

        }

        #region Button Clicks



        #endregion

        //Move to Button Clicks...
    }
}
