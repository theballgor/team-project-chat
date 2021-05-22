using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.Commands;
using System.Windows;
using Client.Model;
using ClientServerLibrary.DbClasses;
using Client.Store;
using ClientLibrary;

namespace Client.ViewsModel
{
    class LoginViewModel : ViewModelBase
    {
        // Constructor
        public LoginViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            LoginModel.LoginSucces += LoginModel_LoginSucces;
        }

        // Fields
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
        private readonly NavigationStore navigationStore;
        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        // Commands
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

        // Callback
        private void LoginModel_LoginSucces(object sender, EventArgs e)
        {
            Console.WriteLine("Logined: " + ((e as ViewModelEventArgs).Content as User).Email);
        }
        protected virtual void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
