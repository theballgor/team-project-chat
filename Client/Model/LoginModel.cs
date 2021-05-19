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
    internal static class LoginModel
    {
        static LoginModel()
        {
            if (!ClientModel.IsConnected)
            {
                ClientModel.CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetFreeTcpPort());
                ClientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
            }
        }

        static private string email;
        static private string password;

        static public string Email
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
        static public string Password
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

        static public User User;

        /// <summary>
        /// Відправка даних залогінення на сервер та отримує i розпаковує отримані дані
        /// </summary>
        static public void TryLogin()
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

                    /// ГОЛОВНА СТОРІНКА ПОЧИНАЄТЬСЯ ТУТ З ДАНИМИ У ОБ'ЄКТІ [response]
                    /// 

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }



        static private void Validate()
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
