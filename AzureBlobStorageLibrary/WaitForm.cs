using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    public partial class WaitForm : Form
    {
        public WaitForm (string displayMsg)
        {
            InitializeComponent();
            MsgLabel.Text = displayMsg;
            Show();
            Update();
        }
    }
}
