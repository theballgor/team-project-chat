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
        private void RegisterUser(User user)
        {
            Console.WriteLine(dbManager.CreateUser(user));
        }
        private void LoginUser(User user)
        {
            dbManager.CheckLogin(user);
        }


        private void SendMessage(TcpClient receiverClient, byte[] message)
        {
            receiverClient.GetStream().Write(message, 0, message.Length);
        }
        private void SendMessage(List<TcpClient> receiverClients, byte[] message)
        {
            foreach (TcpClient receiverClient in receiverClients)
            {
                foreach (KeyValuePair<string, TcpClient> client in connectedClients)
                {
                    TcpClient tmpClient = client.Value;
                    if (receiverClient == tmpClient)
                    {
                        tmpClient.GetStream().Write(message, 0, message.Length);
                        Console.WriteLine("Send");
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
        public void AbortConnection(string login)
        {
            try
            {
                for (int i = 0; i < connectedClients.Count; i++)
                {
                    if (connectedClients[i].Key == login)
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
