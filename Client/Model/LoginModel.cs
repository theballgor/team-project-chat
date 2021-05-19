using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using ClientLibrary;

namespace Client.Model
{
    static class LoginModel
    {
        static LoginModel()
        {
            if (ClientModel.IsConnected)
            {
                ClientModel.CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetFreeTcpPort());
                ClientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
            }
        }

        public static User user;

        private static string email;
        private static string password;

        public static string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public static string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public static void TryLogin()
        {
            Task.Run(() =>
            {
                try
                {
                    Validate();
                    User user = new User
                    {
                        Email = email,
                        Password = Cryptography.Encrypt(password),
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

                    ClientModel.SendMessage(message);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }
        private static void Validate()
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
