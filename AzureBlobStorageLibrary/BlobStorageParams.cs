using System;

namespace AzureBlobStorageLibrary
{
    public class BlobStorageParams
    {
        public enum MsgType { mtNone, mtOnEnd, mtAllTime };
        public string connectionString, containerName, localFileName, queueName;
        public bool deleteFile, asyncProcess;
        public MsgType messsageType;

        public static string GetTextParameter(string text, string parName)
        {
            foreach (string line in text.Split(new char[] { '\n', '\r' }))
                if (line.IndexOf(parName + "=", StringComparison.OrdinalIgnoreCase) == 0)
                    return line.Substring(parName.Length + 1);
            return "";
        }
        public static bool GetBoolParameter(string text, string parName, bool defVal)
        {
            var cRes = GetTextParameter(text, parName).Trim();
            if (String.IsNullOrEmpty(cRes)) return defVal;
            return (cRes[0] == 'T') || (cRes[0] == 't');
        }
        public static MsgType GetMsgTypeParameter(string text, string parName)
        {
            var cRes = GetTextParameter(text, parName).Trim();
            if (!String.IsNullOrEmpty(cRes))
            {
                if ((cRes[0] == 'E') || (cRes[0] == 'e')) return MsgType.mtOnEnd;
                if ((cRes[0] == 'A') || (cRes[0] == 'a')) return MsgType.mtAllTime;
            }
            return MsgType.mtNone;
        }
        public BlobStorageParams(string fpars)
        {
            connectionString = GetTextParameter(fpars, "connectionString");
            containerName = GetTextParameter(fpars, "containerName");
            queueName = GetTextParameter(fpars, "queueName");
            localFileName = GetTextParameter(fpars, "localFileName");
            deleteFile = GetBoolParameter(fpars, "deleteFile", String.IsNullOrEmpty(containerName));
            asyncProcess = GetBoolParameter(fpars, "asyncProcess", false);
            messsageType = GetMsgTypeParameter(fpars, "messageType");
        }
    }
}