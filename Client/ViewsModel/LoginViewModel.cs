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

        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }

        // Constructor
        public LoginViewModel(NavigationStore navigationStore)
        {
            LoginModel.LoginSucces += LoginModel_LoginSucces;
            navigationService = new NavigationService<AccountViewModel>(navigationStore, () => new AccountViewModel(navigationStore));

            LoginCommand = new LoginCommand(this, navigationService);
            RegistrationCommand = new RegistrationCommand(this, new NavigationService<RegistrationViewModel>(navigationStore, () => new RegistrationViewModel(navigationStore)));
        }

        // Callback
        private void LoginModel_LoginSucces(object sender, EventArgs e)
        {
            User user = ((e as ViewModelEventArgs).Content as User);
            if (user != null)
            {
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
