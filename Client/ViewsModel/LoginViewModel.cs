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
    class LoginViewModel : INotifyPropertyChanged
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
                OnPropertyChanged("Email");
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


        public RelayCommand Login
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
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        private void Send()
        {
            MessageBox.Show("Ok");
        }

        private void Validate()
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
