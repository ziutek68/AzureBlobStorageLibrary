using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class OldestFileFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        private int GetOldestFromBlobStorage()
        {
            MessageOnBegin();
            try
            {
                BlobClient blobClient = BlobStorageUtility.GetOldestBlob(blobParams);
                if (blobClient == null)
                    return BlobStorageUtility.ShowContainerWarning(1, blobParams);
                if (!blobClient.Exists())
                    return BlobStorageUtility.ShowContainerWarning(2, blobParams);
                BlobDownloadInfo download = blobClient.Download();
//                blobParams.localFileName = Path.Combine(Path.GetDirectoryName(blobParams.localFileName), blobClient.Name);
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
                waitForm = new WaitForm($"Trwa pobieranie najstarszego pliku z kontenera {blobParams.containerName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Pobrano najstarszy plik z kontenera {blobParams.containerName} pod nazw¹ {blobParams.localFileName}", "AzureBlobStorageLibrary");
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
                    Task.Run(() => GetOldestFromBlobStorage());
                    fouts = $"Result=Trwa pobieranie najstarszego pliku z kontenera {blobParams.containerName}";
                    res = 0;
                }
                else
                {
                    res = GetOldestFromBlobStorage();
                    if (res == 0)
                        fouts = $"Result=Pobrano najstarszy plik z kontenera {blobParams.containerName} pod nazw¹ {blobParams.localFileName}";
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