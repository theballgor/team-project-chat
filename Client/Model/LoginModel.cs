using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using ClientLibrary;
using Client.Stores;

namespace Client.Model
{
    public class LoginModel
    {
        private readonly ClientModelStore _clientModelStore;
        private readonly ClientModel clientModel;


        public LoginModel(ClientModelStore clientModelStore)
        {
            clientModel = new ClientModel();
            ConnectionToServer();
        }

        public void ConnectionToServer()
        {
           clientModel.CreateClientEndpoint(GlobalVariables.LocalIP, clientModel.GetFreeTcpPort());
           clientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
           clientModel.StartListening();
        }

        public void TryLogin(string email, string password)
        {
            Task.Run(() =>
            {
                try
                {
                    Validate(email, password);
                    User user = new User
                    {
                        Email = email,
                        Password = password,
                        Avatar = null,
                        ConversationConnections = null,
                        Description = null,
                        Id = -1,
                        PhoneNumber = null,
                        Status = UserStatus.Online,
                        Username = null
                    };

                    ClientServerMessage message = new ClientServerMessage { Content = user };
                    message.ActionType = ActionType.LogInUser;

                    clientModel.SendMessage(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        private void Validate(string email, string password)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(email);
                if (string.IsNullOrEmpty(password) || (password.Length < 8 || password.Length > 16))
                    throw new ArgumentException("Invalid email or password");
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid email or password");
            }
        }
    }
}
