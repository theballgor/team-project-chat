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
}
