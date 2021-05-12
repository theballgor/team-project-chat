using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.Commands;
using System.Windows;
using Client.Model;

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

                    }
                    catch (ArgumentException exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

    }
}
