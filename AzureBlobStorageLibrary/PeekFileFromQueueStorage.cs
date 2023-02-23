using Azure.Storage.Queues;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class PeekFileFromQueueStorage
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        public int Execute(string _1, string fpars, ref string fouts)
        {
            int res = -1;
            try
            {
                blobParams = new BlobStorageParams(fpars);
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => GetFileFromQueueStorage());
                    fouts = $"Result=Trwa pobieranie {blobParams.localFileName} z kolejki {blobParams.queueName}";
                    res = 0;
                }
                else
                {
                    res = GetFileFromQueueStorage();
                    if (res == 0)
                        fouts = $"Result=Jest lokalnie plik {blobParams.localFileName} z kolejki {blobParams.queueName}";
                }
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return res;
        }
        private int GetFileFromQueueStorage()
        {
            MessageOnBegin();
            try
            {
                QueueClient queueClient = new QueueClient(blobParams.connectionString, blobParams.queueName);
                if (!queueClient.Exists())
                    return BlobStorageUtility.ShowQueueWarning(1, blobParams);
                var response = queueClient.PeekMessage().Value;
                if (response == null)
                    return BlobStorageUtility.ShowQueueWarning(2, blobParams);
                File.WriteAllText(blobParams.localFileName, response.Body.ToString());
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
                waitForm = new WaitForm($"Trwa pobieranie { blobParams.localFileName} z kolejki { blobParams.queueName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Jest lokalnie plik {blobParams.localFileName} z kolejki {blobParams.queueName}", "AzureBlobStorageLibrary");
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm.Close();
        }
    }
}
