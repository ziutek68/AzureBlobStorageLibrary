using Azure.Storage.Blobs;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class SendFileToBlobStorage
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        private void TransmitFileToBlobStorage()
        {
            MessageOnBegin();
            try
            {
                BlobClient blobClient = BlobStorageUtility.GetBlobClient(blobParams, true);
                blobClient.Upload(blobParams.localFileName, true);
                if (blobParams.deleteFile) File.Delete(blobParams.localFileName);
            }
            finally
            {
                MessageOnFinish();
            }
        }
        private void MessageOnBegin()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm = new WaitForm($"Trwa wysyłanie { blobParams.localFileName} do kontenera { blobParams.containerName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Wysłano plik {blobParams.localFileName} do kontenera {blobParams.containerName}", "AzureBlobStorageLibrary");
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm.Close();
        }
        private async void TransmitFileToBlobStorageAsync()
        {
            MessageOnBegin();
            BlobClient blobClient = BlobStorageUtility.GetBlobClient(blobParams, true);
            await blobClient.UploadAsync(blobParams.localFileName, true);
            if (blobParams.deleteFile) File.Delete(blobParams.localFileName);
            MessageOnFinish();
        }
        public int Execute(string _1, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => TransmitFileToBlobStorage());
                    fouts = $"Result=Trwa wysyłanie {blobParams.localFileName} do kontenera {blobParams.containerName}";
                }
                else
                {
                    TransmitFileToBlobStorage();
                    fouts = $"Result=Wysłano plik {blobParams.localFileName} do kontenera {blobParams.containerName}";
                }
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
    }
}