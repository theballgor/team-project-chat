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
using Client.Services;
using System.Windows.Input;

namespace Client.ViewsModel
{
    class LoginViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;
        private readonly NavigationService<AccountViewModel> navigationService;

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

        private ICommand _loginCommand;
        private ICommand _registrationCommand;


        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(parameter =>
                {
                    LoginModel.TryLogin();
                }));
            }
        }
        public ICommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ?? (_registrationCommand = new RelayCommand(parameter =>
                {
                    new NavigationService<RegistrationViewModel>(navigationStore, () => new RegistrationViewModel(navigationStore)).Navigate();
                }));
            }
        }

        // Constructor
        public LoginViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            LoginModel.LoginSucces += LoginModel_LoginSucces;
            navigationService = new NavigationService<AccountViewModel>(navigationStore, () => new AccountViewModel(navigationStore));
        }

        // Callback
        private void LoginModel_LoginSucces(object sender, EventArgs e)
        {
            User user = ((e as ViewModelEventArgs).Content as User);
            if (user != null)
            {
                AccountModel.User = user;
                navigationService.Navigate();
                Console.WriteLine("Logined: " + user.Email);
            }
            else
            {
                Console.WriteLine("Failed to login");
            }
        }
    }
}
