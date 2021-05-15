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

namespace Client.Model
{
    /// <summary>
    /// LOGIC
    /// зв'язок клієнта з сервером
    /// </summary>
    public static class ClientModel
    {
        private static TcpClient client;


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
            if (client != null)
                client.Connect(new IPEndPoint(ipAddress, port));
        }

        public static void StartListening(ref ClientServerMessage message)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] data = new byte[128];
                while (true)
                {
                    List<byte> fullData = new List<byte>();
                    do
                    {
                        stream.Read(data, 0, data.Length);
                        fullData.AddRange(data);
                    } while (stream.DataAvailable);

                    message = ClientServerDataManager.Deserialize(fullData.ToArray());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }

        }

        ////view-model part
        //private static void HandleMessage(ref ClientServerMessage message)
        //{
        //    message.Content = "Hello";

        //    ///
        //    ///
        //    ///
        //}




        //Send message
        public static void SendMessage(ClientServerMessage message)
        {
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
    }
}
