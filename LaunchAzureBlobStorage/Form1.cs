using System;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    public partial class Form1 : Form
    {
        readonly string fdatas = "APLIKACJA=LaunchAzureBlobStorage";
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            var myFunc = new SendFileToBlobStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            var myFunc = new GetFileFromBlobStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            var myFunc = new EraseFileFromBlobStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            var myFunc = new ListFilesFromBlobStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            var myFunc = new OldestFileFromBlobStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            var myFunc = new SendFileToQueueStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            var myFunc = new ReceiveFileFromQueueStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            var myFunc = new PeekFileFromQueueStorage();
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
    }
}
