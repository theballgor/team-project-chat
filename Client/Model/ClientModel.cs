using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClientServerLibrary;

namespace Client.Model
{
    /// <summary>
    /// LOGIC
    /// зв'язок клієнта з сервером
    /// </summary>
    class ClientModel
    {
        private TcpClient client = new TcpClient();

        public ClientModel(IPEndPoint clientIEP)
        {
            try
            {
                client = new TcpClient(clientIEP);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Connect(IPEndPoint serverIEP)
        {
            client.Connect(serverIEP);
        }

        public void StartListening()
        {
            Task.Run(new Action(() =>
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
                        HandleMessage(fullData.ToArray());
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error");
                }
            }));
        }

        //view-model part
        private void HandleMessage(byte[] arr)
        {
            ClientServerMessage message = ClientServerMessageFormatter.Deserialize(arr);


            ////        TESTING
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("From:\t");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message.Content.ToString());
            ////
        }

        //Send message
        private void SendMessage(ClientServerMessage message)
        {
            Task.Run(new Action(() =>
            {
                try
                {
                    byte[] arr = ClientServerMessageFormatter.Serialize(message);
                    NetworkStream stream = client.GetStream();
                    lock (this)
                    {
                        stream.Write(arr, 0, arr.Length);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message, "Send error");
                }
            }));
        }


        //Get free tcp port
        public static int GetFreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        ////Get local IP address
        //public static string GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ip.ToString();
        //        }
        //    }
        //    throw new Exception("No network adapters with an IPv4 address in the system!");
        //}
    }
}
