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

        public string Email
        {
            get
            {
                return LoginModel.Email;
            }
            set
            {
                LoginModel.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get
            {
                return LoginModel.Password;
            }
            set
            {
                LoginModel.Password = value;
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
                        LoginModel.TryLogin();



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
