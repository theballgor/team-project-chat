using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.Commands;
using System.Windows;

namespace Client.ViewsModel
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        private string email;
        private string nickname;
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
                OnPropertyChanged("Email");
            }
        }

        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                nickname = value;
                OnPropertyChanged("Nickname");
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
                OnPropertyChanged("Password");
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
                OnPropertyChanged("VerifyPassword");
            }
        }

        public RelayCommand Register
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    try
                    {
                        Validate();
                        Send();
                    }
                    catch (ArgumentException exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                });
            }
        }

        private void Send()
        {
            System.Windows.MessageBox.Show(nickname);
        }

        private void Validate()
        {
            if (!ValidateString(nickname, 4, 25))
                throw new ArgumentException("Invalid Nickname");

            if (!ValidateString(password, 8, 16))
                throw new ArgumentException("Invalid Password");

            if (password != verifyPassword)
                throw new ArgumentException("Verify the password");

            if (!ValidateEmail())
                throw new ArgumentException("Invalid Email");
        }

        private bool ValidateString(string str, int from, int to)
        {
            if (string.IsNullOrEmpty(str) || (str.Length < from || str.Length > to))
                return false;
            return true;
        }

        private bool ValidateEmail()
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

    }
}
