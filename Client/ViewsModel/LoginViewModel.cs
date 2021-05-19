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
    class LoginViewModel : INotifyPropertyChanged
    {
        private LoginModel loginModel = new LoginModel();

        public string Email
        {
            get
            {
                return loginModel.Email;
            }
            set
            {
                loginModel.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get
            {
                return loginModel.Password;
            }
            set
            {
                loginModel.Password = value;
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
                        loginModel.TryLogin();
                    }
                    catch (ArgumentException exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }
        public RelayCommand RegistrationWindow
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
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
