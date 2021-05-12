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
    static class RegistrationModel
    {

        private static ClientServerMessage Pack(string username, string email, string password)
        {
            password = Cryptography.Encrypt(password);
            User user = new User { Email = email, Password = password, Username = username };

            return new ClientServerMessage { Content = user, Date = DateTime.Now, MessageType = MessageType.RegistrationRequest };
        }

        private static void Validate(string username, string password, string verifyPassword, string email)
        {
            ValidateString(username, 4, 25, "Invalid Nickname");
            ValidateString(password, 8, 16, "Invalid Password");

            if (password != verifyPassword)
                throw new ArgumentException("Verify the password");

            if (!ValidateEmail(email))
                throw new ArgumentException("Invalid Email");
        }

        private static bool ValidateString(string str, int from, int to, string errorMessage)
        {
            if (string.IsNullOrEmpty(str) || (str.Length < from || str.Length > to))
                throw new ArgumentException(errorMessage);
            return true;
        }

        private static bool ValidateEmail(string email)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static ClientServerMessage Handle(string username, string password, string verifyPassword, string email)
        {
            try
            {
                Validate(username, password, verifyPassword, email);
                ClientServerMessage message = Pack(username, email, password);
                
                return message;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
