using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;

namespace Client.Model
{
    /// <summary>
    /// LOGIC
    /// зв'язок клієнта з сервером
    /// </summary>
    public static class ClientModel
    {
        private static TcpClient client;
        public static bool IsConnected => client.Connected;


        private static ClientServerMessage Listen()
        {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[128];
            List<byte> fullData = new List<byte>();
            do
            {
                stream.Read(data, 0, data.Length);
                fullData.AddRange(data);
            } while (stream.DataAvailable);

            return ClientServerDataManager.Deserialize(fullData.ToArray());
        }
        public static void StartListening()
        {
            Task.Run(() =>
            {
                try
                {
                    while (true)
                        DataWorker.Handle(Listen());
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error");
                }

            });
        }
        public static void SendMessage(ClientServerMessage message)
        {
            if (client != null && client.Connected)
                Task.Run(new Action(() =>
                {
                    try
                    {
                        byte[] arr = ClientServerDataManager.Serialize(message);
                        NetworkStream stream = client.GetStream();
                        stream.Write(arr, 0, arr.Length);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error: " + exc.Message, "Send error");
                    }
                }));
        }






        public static int GetFreeTcpPort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
        public static void CreateClientEndpoint(IPAddress clientIpAddress, int clientPort)
        {
            try
            {
                client = new TcpClient(new IPEndPoint(clientIpAddress, clientPort));
            }
            catch (Exception)
            {

            }
        }
        public static void Connect(IPAddress ipAddress, int port)
        {
            if (client != null && !IsConnected)
                client.Connect(new IPEndPoint(ipAddress, port));
        }
    }
}
