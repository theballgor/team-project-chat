using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer
{
    [Serializable]
    public enum MessageType
    {
        Text,
        Audio,
        File
    }

    [Serializable]
    public struct ClientServerMessage
    {
        public ClientServerMessage(
            object content,
            object additionalContent,
            MessageType messageType,
            DateTime date)
        {
            Content = content;
            AdditionalContent = additionalContent;
            MessageType = messageType;
            Date = date;
        }

        public object Content { get; set; }
        public object AdditionalContent { get; set; }
        public MessageType MessageType { get; set; }
        public DateTime Date { get; set; }
    }

    public static class GlobalVariables
    {
        public static readonly int ServerPort = 40000;
        public static readonly IPAddress LocalIP = IPAddress.Parse("127.0.0.1");
    }


    public static class ClientServerMessageFormatter
    {
        public static ClientServerMessage Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ClientServerMessage serializedData;
            using (MemoryStream stream = new MemoryStream(data))
            {
                serializedData = (ClientServerMessage)formatter.Deserialize(stream);
            }
            return serializedData;
        }

        public static byte[] Serialize(ClientServerMessage data)
        {
            byte[] serializedData = new byte[4096];
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                serializedData = stream.ToArray();
            }
            return serializedData;
        }

    }
}
