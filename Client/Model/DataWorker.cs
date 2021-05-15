using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClientServerLibrary;

namespace Client.Model
{
    static class DataWorker
    {
        private static ClientServerMessage message;

        static DataWorker()
        {
            ClientModel.CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetFreeTcpPort());
            ClientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
            Task.Run(() => {
                ClientModel.StartListening(ref message);
            });
        }


        public static ClientServerMessage getMessage()
        {
            return message;
        }
    }
}
