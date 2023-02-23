using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    public class GetFileFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        private int TransmitFileFromBlobStorage()
        {
            MessageOnBegin();
            try
            {
                BlobClient blobClient = BlobStorageUtility.GetBlobClient(blobParams, false);
                if (blobClient == null)
                    return BlobStorageUtility.ShowContainerWarning(1, blobParams);
                if (!blobClient.Exists())
                    return BlobStorageUtility.ShowContainerWarning(2, blobParams);
                BlobDownloadInfo download = blobClient.Download();
                using (FileStream fileStream = File.OpenWrite(blobParams.localFileName))
                    download.Content.CopyTo(fileStream);
                if (blobParams.deleteFile) blobClient.Delete();
                return 0;
            }
            finally
            {
                MessageOnFinish();
            }
        }
        private void MessageOnBegin()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm = new WaitForm($"Trwa pobieranie { blobParams.localFileName} z kontenera { blobParams.containerName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Pobrano plik {blobParams.localFileName} z kontenera {blobParams.containerName}", "AzureBlobStorageLibrary");
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm.Close();
        }
        public int Execute(string _1, string fpars, ref string fouts)
        {
            int res = -1;
            try
            {
                blobParams = new BlobStorageParams(fpars);
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => TransmitFileFromBlobStorage());
                    fouts = $"Result=Trwa pobieranie {blobParams.localFileName} z kontenera {blobParams.containerName}";
                    res = 0;
                }
                else
                {
                    res = TransmitFileFromBlobStorage();
                    if (res == 0)
                        fouts = $"Result=Pobrano plik {blobParams.localFileName} z kontenera {blobParams.containerName}";
                }
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return res;
        }
    }
}