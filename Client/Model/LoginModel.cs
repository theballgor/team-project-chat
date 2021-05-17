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
     class LoginModel
    {
        private string email;
        private string password;

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

        public void Send()
        {
            DataWorker.Test();

            User user = new User {
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
        }

        public void LoginUser(User user)
        {
            Console.WriteLine(user.Email);
        }

        public void Validate()
        {
            ValidateString(password, "Invalid data", 8, 16);
            ValidateEmail("Invalid data");
        }

        private void ValidateString(string str, string exceptionMessage, int from, int to)
        {
            if (string.IsNullOrEmpty(str) || (str.Length < from || str.Length > to))
                throw new ArgumentException(exceptionMessage);
        }

        private void ValidateEmail(string exceptionMessage)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}
