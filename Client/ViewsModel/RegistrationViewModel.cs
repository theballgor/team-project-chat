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
        private RegistrationModel registrationModel = new RegistrationModel();

        public string Email
        {
            get
            {
                return registrationModel.Email;
            }
            set
            {
                registrationModel.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Username
        {
            get
            {
                return registrationModel.Username;
            }
            set
            {
                registrationModel.Username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Password
        {
            get
            {
                return registrationModel.Password;
            }
            set
            {
                registrationModel.Password = value;
                OnPropertyChanged("Password");
            }
        }
        public string VerifyPassword
        {
            get
            {
                return registrationModel.VerifyPassword;
            }
            set
            {
                registrationModel.VerifyPassword = value;
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
                        registrationModel.TryRegister();
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
