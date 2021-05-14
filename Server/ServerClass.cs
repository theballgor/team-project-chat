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
    public partial class ServerClass
    {
        private DbManager dbManager;
        private TcpListener server;
        private List<KeyValuePair<string, TcpClient>> connectedClients;
        private readonly object locker;
        public ServerClass(IPEndPoint serverIEP)
        {
            server = new TcpListener(serverIEP);
            locker = new object();
            lock (locker)
                connectedClients = new List<KeyValuePair<string, TcpClient>>();
            dbManager = new DbManager();
   
        }

        public void Connect()
        {
            server.Start();
            Console.WriteLine($"Server started\t\t{server.LocalEndpoint}");

            Task.Run(() =>
            {
                while (true)
                {
                    TcpClient currentClient = server.AcceptTcpClient();
                    Console.WriteLine("Connnected\t\t" + currentClient.Client.RemoteEndPoint);
                    Task.Run(() =>
                    {
                        ConversationHandler(currentClient);
                    });
                }
            });
        }

        private void ConversationHandler(TcpClient client)
        {
            try
            {
                while (true)
                {
                    byte[] data = ClientServerDataManager.TcpClientDataReader(client);
                    ClientServerMessage message = ClientServerDataManager.Deserialize(data);
                    switch (message.ActionType)
                    {
                        case ActionType.SendText:

                            break;
                        case ActionType.SendAudio:

                            break;
                        case ActionType.SendFile:

                            break;
                        case ActionType.RegisterUser:
                            RegisterUser((User)message.Content);
                            break;
                        case ActionType.LogInUser:

                            break;
                        case ActionType.CreateConversation:

                            break;
                    }

                }
            }
            catch (Exception e)
            {
                AbortConnection(client);
                Console.WriteLine(e.Message);
            }
        }
    }
}