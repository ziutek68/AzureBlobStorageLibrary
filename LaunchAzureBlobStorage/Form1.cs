using System;
using System.Windows.Forms;
using AzureBlobStorageLibrary;

namespace LaunchAzureBlobStorage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var myFunc = new SendFileToBlobStorage();
            var fdatas = "APLIKACJA=LaunchAzureBlobStorage";
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myFunc = new GetFileFromBlobStorage();
            var fdatas = "APLIKACJA=LaunchAzureBlobStorage";
            var fpars = paramsMemo.Text;
            string fRes = "";
            myFunc.Execute(fdatas, fpars, ref fRes);
            resultMemo.Text = fRes;
        }
    }
}
