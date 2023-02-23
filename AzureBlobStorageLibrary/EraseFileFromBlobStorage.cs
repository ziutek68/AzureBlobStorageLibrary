using Azure.Storage.Blobs;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    public class EraseFileFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        private int DeleteFileFromBlobStorage()
        {
            MessageOnBegin();
            try
            {
                BlobClient blobClient = BlobStorageUtility.GetBlobClient(blobParams, false);
                if (blobClient == null)
                    return BlobStorageUtility.ShowContainerWarning(1, blobParams);
                if (!blobClient.Exists())
                    return BlobStorageUtility.ShowContainerWarning(1, blobParams);
                blobClient.Delete();
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
                waitForm = new WaitForm($"Trwa usuwanie { blobParams.localFileName} z kontenera { blobParams.containerName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Usunięto plik {blobParams.localFileName} z kontenera {blobParams.containerName}", "AzureBlobStorageLibrary");
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
                    Task.Run(() => DeleteFileFromBlobStorage());
                    fouts = $"Result=Trwa usuwanie {blobParams.localFileName} z kontenera {blobParams.containerName}";
                    res = 0;
                }
                else
                {
                    res = DeleteFileFromBlobStorage();
                    if (res == 0)
                        fouts = $"Result=Usunięto plik {blobParams.localFileName} z kontenera {blobParams.containerName}";
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