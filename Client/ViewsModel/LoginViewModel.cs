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

        private void Validate()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Incorrect data");
        }

        private void Send()
        {
            MessageBox.Show("Ok");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
