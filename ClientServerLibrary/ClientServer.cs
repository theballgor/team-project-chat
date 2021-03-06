using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ClientServerLibrary
{
    [Serializable]
    public enum ActionType
    {
        SendConversationMessage,
        SendFriendRequest,
        RegisterUser,
        LogInUserByEmail,
        LogInUserByUsername,
        CreateConversation,
        JoinConversation,
        GetConversationMessages,
        GetUserConversations,
        GetConversationUsers,
        GetUserFriendships,
        GetUserInfo,
        FatalError,
    }

    // Enum відповідальний за реєстрацію
    public enum RegistrationResult
    { 
        //Якщо успішно
        Success,
        //Якщо пароль невірний
        PasswordDoNotMatch,
        //Якщо e-mail уже існує
        EmailAlreadyExists,
        //Якщо user (nick name) уже існує
        UserNameAlreadyExists
    }




    [Serializable]
    public struct ClientServerMessage
    {
        public ClientServerMessage(
            object content,
            object additionalContent,
            ActionType actionType,
            DateTime date)
        {
            Content = content;
            AdditionalContent = additionalContent;
            ActionType = actionType;
            Date = date;

        }
        public object Content { get; set; }
        public object AdditionalContent { get; set; }
        public ActionType ActionType { get; set; }
        public DateTime Date { get; set; }
    }

    public static class GlobalVariables
    {
        public static readonly int ServerPort = 40000;
        public static readonly IPAddress LocalIP = IPAddress.Parse("127.0.0.1");
    }

    public static class ClientServerDataManager
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
        public static byte[] TcpClientDataReader(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[128];
            List<byte> messageInBytes = new List<byte>();
            do
            {
                stream.Read(data, 0, data.Length);
                messageInBytes.AddRange(data);
            } while (stream.DataAvailable);
            return messageInBytes.ToArray();
        }
        
    }
}