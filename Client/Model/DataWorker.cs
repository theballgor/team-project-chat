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
        static public void Handle(ClientServerMessage message)
        {
            switch (message.ActionType)
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

                    LoginModel.User = LoginUser(message);

                    break;
                case ActionType.CreateConversation:
                    break;
                case ActionType.JoinConversation:
                    break;
                case ActionType.AddFriend:
                    break;
                case ActionType.GetConversationMessages:
                    break;
                case ActionType.FatalError:
                    break;
                default:
                    break;
            }
        }

        static private User LoginUser(ClientServerMessage message)
        {
            if (message.Content == null)
                throw new ArgumentException((string)message.AdditionalContent);

            return (message.Content as User);
        }



    }
}
