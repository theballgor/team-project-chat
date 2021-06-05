using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary.DbClasses;
using ClientServerLibrary;
using ClientLibrary;
using System.Windows;

namespace Client.Model
{
    static class RegistrationModel
    {
        // Event
        public static event EventHandler RegisterSucces;

        // Fields
        private static string email;
        private static string username;
        private static string password;
        private static string verifyPassword;

        public static string Email
        {
            get { return email; }
            set { email = value; }
        }
        public static string Username
        {
            get { return username; }
            set { username = value; }
        }
        public static string Password
        {
            get { return password; }
            set { password = value; }
        }
        public static string VerifyPassword
        {
            get { return verifyPassword; }
            set { verifyPassword = value; }
        }


        static RegistrationModel()
        {}

        // Notyfier
        public static void Notify(object result)
        {
            RegistrationResult registrationResult = (RegistrationResult)result;
            switch (registrationResult)
            {
                case RegistrationResult.Success:
                    RegisterSucces(null, new ViewModelEventArgs { Content = result });
                    break;
   
                default:
                    MessageBox.Show(registrationResult.ToString());
                    break;
            }
            
        }

        public static void TryRegister()
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
                        Username = username
                    };

                    ClientServerMessage message = new ClientServerMessage { Content = user };
                    message.ActionType = ActionType.RegisterUser;

                    ClientModel.GetInstance().SendMessageAsync(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        private static void Validate()
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
