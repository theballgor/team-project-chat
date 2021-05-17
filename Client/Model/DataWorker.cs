using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;

namespace Client.Model
{
    public static class DataWorker
    {
        static DataWorker()
        {
            ClientModel.CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetFreeTcpPort());
            ClientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
            while (true)
                Handler();
        }




        public static void Test()
        {

        }


        private static  void Handler()
        {
            foreach (ClientServerMessage item in ClientModel.Messages)
            {
                Task.Run(() => 
                {
                    switch (item.ActionType)
                    {
                        case ActionType.SendText:
                            break;
                        case ActionType.SendAudio:
                            break;
                        case ActionType.SendFile:
                            break;
                        case ActionType.RegisterUser:
                            break;
                        case ActionType.LogInUser:
                            LoginModel.LoginUser((User)item.Content);
                            ClientModel.Messages.Remove(item);
                            break;
                        case ActionType.CreateConversation:
                            break;
                        case ActionType.GetConversationMessages:
                            break;
                        case ActionType.Error:
                            break;
                        case ActionType.Success:
                            break;
                        default:
                            break;
                    }
                });
            }
        }

    }
}
