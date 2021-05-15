using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.Commands;
using System.Windows;
using Client.Model;
using ClientServerLibrary;

namespace Client.ViewsModel
{
    class RegistrationViewModel : INotifyPropertyChanged
    {

        
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
                OnPropertyChanged("Email");
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
                OnPropertyChanged("Username");
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



        public RelayCommand RegisterButton
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    try
                    {
                        ClientServerMessage message = RegistrationModel.Handle(username, password, verifyPassword, email);
                        ClientModel.SendMessage(message);

                    }
                    catch (ArgumentException exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                });
            }
        }

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
