using Azure.Messaging.ServiceBus;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class GetFileFromQueueServiceBus
    {
        private BlobStorageParams blobParams;
        public int Execute(string _1, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                GetFileToQueueServiceBusAsync().GetAwaiter();
                fouts = $"Result=Zakończono subskrypcję kolejki {blobParams.queueName} w S.B.";
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
        private async Task GetFileToQueueServiceBusAsync()
        {
            var client = new ServiceBusClient(blobParams.connectionString);
            ServiceBusProcessor processor = client.CreateProcessor(blobParams.queueName, new ServiceBusProcessorOptions());
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
            await processor.StartProcessingAsync();
            MessageBox.Show($"Trwa subskrypcja kolejki {blobParams.queueName} w S.B.", "AzureBlobStorageLibrary");
            await processor.CloseAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            MessageBox.Show(args.Exception.ToString(), "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return Task.CompletedTask;
        }
        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            File.WriteAllText(blobParams.localFileName, args.Message.Body.ToString());
            if(blobParams.deleteFile)
                await args.CompleteMessageAsync(args.Message);
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Pobrano plik {blobParams.localFileName} z kolejki {blobParams.queueName} w S.B.", "AzureBlobStorageLibrary");

        }
    }
}
