using System;
using System.IO;
using System.Runtime.InteropServices;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobStorageLibrary
{
    static class TextUtility
    {
        public static string GetTextParameter(string text, string parName)
        {
            foreach (string line in text.Split(new char[] { '\n', '\r' }))
                if (line.IndexOf(parName + "=", StringComparison.OrdinalIgnoreCase) == 0)
                    return line.Substring(parName.Length + 1);
            return "";
        }
        public static bool GetBoolParameter(string text, string parName)
        {
            var cRes = TextUtility.GetTextParameter(text, parName).Trim();
            return (cRes == "T") || (cRes == "t");
        }
    }
    [ComVisible(true)]
    public class SendFileToBlobStorage
    {
        private string connectionString, containerName, localFileName;
        private bool deleteFile;

        private void TransmitFileToBlobStorage()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (containerClient == null)
             containerClient = blobServiceClient.CreateBlobContainer(containerName);
            var fileName = Path.GetFileName(localFileName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            blobClient.Upload(localFileName, true);
            if (deleteFile) File.Delete(localFileName);
        }
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            int res = -1;
            try
            {
                connectionString = TextUtility.GetTextParameter(fpars, "connectionString");
                containerName = TextUtility.GetTextParameter(fpars, "containerName");
                localFileName = TextUtility.GetTextParameter(fpars, "localFileName");
                deleteFile = TextUtility.GetBoolParameter(fpars, "deleteFile");
                TransmitFileToBlobStorage();
                fouts = $"Result=Wys³ano plik {localFileName} do {containerName}";
                res = 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return res;
        }
    }
    public class GetFileFromBlobStorage
    {
        private string connectionString, containerName, localFileName;
        private bool deleteFile;
        private void TransmitFileFromBlobStorage()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (containerClient == null)
                containerClient = blobServiceClient.CreateBlobContainer(containerName);
            var fileName = Path.GetFileName(localFileName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            BlobDownloadInfo download = blobClient.Download();
            using (FileStream fileStream = File.OpenWrite(localFileName))
                download.Content.CopyTo(fileStream);
            if (deleteFile) blobClient.Delete();
        }
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            int res = -1;
            try
            {
                connectionString = TextUtility.GetTextParameter(fpars, "connectionString");
                containerName = TextUtility.GetTextParameter(fpars, "containerName");
                localFileName = TextUtility.GetTextParameter(fpars, "localFileName");
                deleteFile = TextUtility.GetBoolParameter(fpars, "deleteFile");
                TransmitFileFromBlobStorage();
                fouts = $"Result=Pobrano plik {localFileName} z {containerName}";
                res = 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return res;
        }
    }
}