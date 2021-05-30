using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using ClientLibrary;
using System.ComponentModel;

namespace Client.Model
{
    static class LoginModel
    {
        // Fields
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

        // Event
        public static event EventHandler LoginSucces;

        // Notyfier
        public static void Notify(User user)
        {
            LoginSucces(null, new ViewModelEventArgs { Content = user });
        }

        // Methods
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
                        Password = Cryptography.Encrypt(Password),
                        Avatar = null,
                        ConversationConnections = null,
                        Description = null,
                        Id = -1,
                        PhoneNumber = null,
                        Status = UserStatus.Online,
                        Username = null
                    };

                    Console.WriteLine(user.Password);

                    ClientServerMessage message = new ClientServerMessage { Content = user };
                    message.ActionType = ActionType.LogInUserByEmail;

                    ClientModel.GetInstance().SendMessageAsync(message);

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
