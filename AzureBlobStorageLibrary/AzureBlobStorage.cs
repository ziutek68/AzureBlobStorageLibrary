using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class SendFileToBlobStorage
    {
        private BlobStorageParams blobParams;
        private void TransmitFileToBlobStorage()
        {
            BlobClient blobClient = BlobStorageUtility.GetBlobClient(blobParams, true);
            blobClient.Upload(blobParams.localFileName, true);
            if (blobParams.deleteFile) File.Delete(blobParams.localFileName);
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Wys³ano plik {blobParams.localFileName} do kontenera {blobParams.containerName}", "AzureBlobStorageLibrary");
        }
        private async void TransmitFileToBlobStorageAsync()
        {
            BlobClient blobClient = BlobStorageUtility.GetBlobClient(blobParams, true);
            await blobClient.UploadAsync(blobParams.localFileName, true);
            if (blobParams.deleteFile) File.Delete(blobParams.localFileName);
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Wys³ano plik {blobParams.localFileName} do kontenera {blobParams.containerName}", "AzureBlobStorageLibrary");
        }
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            try
            {     
                blobParams = new BlobStorageParams(fpars);
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => TransmitFileToBlobStorage());
                    fouts = $"Result=Trwa wysy³anie {blobParams.localFileName} do kontenera {blobParams.containerName}";
                }
                else
                {
                    TransmitFileToBlobStorage();
                    fouts = $"Result=Wys³ano plik {blobParams.localFileName} do kontenera {blobParams.containerName}";
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
    public class GetFileFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private int TransmitFileFromBlobStorage()
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
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Pobrano plik {blobParams.localFileName} z kontenera {blobParams.containerName}", "AzureBlobStorageLibrary");
            return 0;
        }
        public int Execute(string fdatas, string fpars, ref string fouts)
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
    public class EraseFileFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private int DeleteFileFromBlobStorage()
        {
            BlobClient blobClient = BlobStorageUtility.GetBlobClient(blobParams, false);
            if (blobClient == null) 
                return BlobStorageUtility.ShowContainerWarning(1, blobParams);
            if (!blobClient.Exists())
                return BlobStorageUtility.ShowContainerWarning(1, blobParams);
            blobClient.Delete();
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Usuniêto plik {blobParams.localFileName} z kontenera {blobParams.containerName}", "AzureBlobStorageLibrary");
            return 0;
        }
        public int Execute(string fdatas, string fpars, ref string fouts)
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
                        fouts = $"Result=Usuniêto plik {blobParams.localFileName} z kontenera {blobParams.containerName}";
                }
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return res;
        }
    }
    public class ListFilesFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private void GetListFromBlobStorage(ref string fouts)
        {
            var blobList = BlobStorageUtility.GetBlobs(blobParams);
            int lp = 0;
            StringBuilder result = new StringBuilder();
            foreach (var blobItem in blobList)
                result.AppendFormat("File{0}={1}\n", ++lp, blobItem.Name);
            fouts = result.ToString();
        }
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                GetListFromBlobStorage(ref fouts);
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
    }
    public class OldestFileFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private int GetOldestFromBlobStorage()
        {
            BlobClient blobClient = BlobStorageUtility.GetOldestBlob(blobParams);
            if (blobClient == null)
                return BlobStorageUtility.ShowContainerWarning(1, blobParams);
            if (!blobClient.Exists())
                return BlobStorageUtility.ShowContainerWarning(2, blobParams);
            BlobDownloadInfo download = blobClient.Download();
            blobParams.localFileName = Path.Combine(Path.GetDirectoryName(blobParams.localFileName), blobClient.Name);
            using (FileStream fileStream = File.OpenWrite(blobParams.localFileName))
                download.Content.CopyTo(fileStream);
            if (blobParams.deleteFile) blobClient.Delete();
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Pobrano najstarszy plik z kontenera {blobParams.containerName} pod nazw¹ {blobParams.localFileName}", "AzureBlobStorageLibrary");
            return 0;
        }
        public int Execute(string fdatas, string fpars, ref string fouts)
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
            return -1;
        }
    }
}