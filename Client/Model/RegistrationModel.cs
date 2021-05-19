using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary.DbClasses;
using ClientServerLibrary;
using ClientLibrary;


namespace Client.Model
{
    internal class RegistrationModel
    {
        public RegistrationModel()
        {
            if (!ClientModel.IsConnected)
            {
                ClientModel.CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetFreeTcpPort());
                ClientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
            }
        }

        private string email;
        private string username;
        private string password;
        private string verifyPassword;

        public string Email
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
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Password
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
        public string VerifyPassword
        {
            get
            {
                return verifyPassword;
            }
            set
            {
                verifyPassword = value;
            }
        }

        public void TryRegister()
        {
            Task.Run(() =>
            {
                try
                {
                    //Validate();
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
                        Username = username
                    };

                    ClientServerMessage message = new ClientServerMessage { Content = user };
                    message.ActionType = ActionType.RegisterUser;

                    ClientModel.SendMessage(message);
                    message = ClientModel.Listen();

                    User response = (message.Content as User);
                    if (response.Id == -1)
                        return;

                    Console.WriteLine(response.Email + "\n" + (message.Content as User).Username);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }
        private void Validate()
        {
            if (string.IsNullOrEmpty(username) || (username.Length < 4 || username.Length > 25))
                throw new ArgumentException("Invalid Nickname");

            if (string.IsNullOrEmpty(password) || (password.Length < 8 || password.Length > 16))
                throw new ArgumentException("Invalid Password");

            if (password != verifyPassword)
                throw new ArgumentException("Verify the password");

            try { System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(email); }
            catch (Exception) { throw new ArgumentException("Invalid Email"); }
        }
    }
}
