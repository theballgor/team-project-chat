using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;


namespace Server
{
   partial class ServerClass
    {
        private void SendMessage(TcpClient receiverClient, byte[] message) => receiverClient.GetStream().Write(message, 0, message.Length);
        private void SendMessage(TcpClient receiverClient, ClientServerMessage message) => SendMessage(receiverClient, ClientServerDataManager.Serialize(message));
        private void SendMessage(List<TcpClient> receiverClients, ClientServerMessage message) => SendMessage(receiverClients, ClientServerDataManager.Serialize(message));
        private int GetUserIdByClient(TcpClient client) => connectedClients.Find(u => u.Value == client).Key;
        private TcpClient GetClientByUserId(int userId) => connectedClients.Find(u => u.Key == userId).Value;
        private List<TcpClient> GetClientsByUsers(User[] users)
        {
            List<TcpClient> clients = new List<TcpClient>();
            foreach (KeyValuePair<int, TcpClient> client in connectedClients)
            {
                foreach (var user in users)
                {
                    if (client.Key == user.Id)
                    {
                        clients.Add(client.Value);
                    }
                }
            }
            return clients;
        }
        private void SendMessage(List<TcpClient> receiverClients, byte[] message)
        {
            foreach (TcpClient receiverClient in receiverClients)
            {
                foreach (KeyValuePair<int, TcpClient> client in connectedClients)
                {
                    TcpClient tmpClient = client.Value;
                    if (receiverClient == tmpClient)
                    {
                        tmpClient.GetStream().Write(message, 0, message.Length);
                        break;
                    }
                }
            }
        }

        public void AbortConnection(TcpClient client)
        {
            try
            {
                for (int i = 0; i < connectedClients.Count; i++)
                {
                    if (connectedClients[i].Value == client)
                    {
                        Console.WriteLine($"User {connectedClients[i].Key} aborted connection");
                        connectedClients.RemoveAt(i);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AbortConnection(int userId)
        {
            try
            {
                for (int i = 0; i < connectedClients.Count; i++)
                {
                    if (connectedClients[i].Key == userId)
                    {
                        Console.WriteLine($"User {connectedClients[i].Key} aborted connection");
                        connectedClients.RemoveAt(i);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
