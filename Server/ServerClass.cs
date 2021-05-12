using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerClass
    {
        private TcpListener server;
        private List<TcpClient> clients;
        public ServerClass(IPEndPoint serverIEP)
        {
            server = new TcpListener(serverIEP);
            clients = new List<TcpClient>();
        }

        public void Connect()
        {
            server.Start();
            Console.WriteLine($"Server started\t\t{server.LocalEndpoint}");

            Task.Run(() =>
            {
                while (true)
                {
                    TcpClient currentUser = server.AcceptTcpClient();
                    clients.Add(currentUser);
                    Console.WriteLine("Connnected\t\t" + currentUser.Client.RemoteEndPoint);

                    Task.Run(() =>
                    {
                        ConversationHandler(currentUser);
                    });
                }
            });
        }

        private void ConversationHandler(TcpClient client)
        {

            while (true)
            {
                byte[] data = GetMethod(client);
                if (data.Length > 0)
                    SendMethod(client, data);
            }
        }

        private byte[] GetMethod(TcpClient client)
        {
            try
            {
                byte[] test = ByteReader(client);
                Console.WriteLine($"{client.Client.RemoteEndPoint}");
                return test;
            }
            catch (Exception)
            {
                clients.Remove(client);
                Console.WriteLine("Disconnected\t\t" + client.Client.RemoteEndPoint);
                throw;
            }
        }

        private void SendMethod(TcpClient client, byte[] message)
        {
            Console.WriteLine("Connected clients:\t" + clients.Count);
            foreach (TcpClient item in clients)
            {
                if (item != client)
                {
                    item.GetStream().Write(message, 0, message.Length);
                    Console.WriteLine("Send");
                }
            }
        }


        private byte[] ByteReader(TcpClient client)
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
