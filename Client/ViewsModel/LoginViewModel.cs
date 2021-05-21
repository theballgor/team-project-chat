using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.Commands;
using System.Windows;
using Client.Model;
using System.Windows.Input;
using Client.Stores;
using Client.Store;
using Client.Services;
using ClientServerLibrary;

namespace Client.ViewsModel
{
    public class LoginViewModel : ViewModelBase
    {

        private string _username;
        public string Username
        {
            get { return _username; }
            set 
            { 
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        LoginModel loginModel;


        public LoginViewModel(ClientModelStore clientModelStore, NavigationStore navigationStore)
        {
            NavigationService<AccountViewModel> navigationService = new NavigationService<AccountViewModel>(
                navigationStore, () => new AccountViewModel(clientModelStore, navigationStore));

            loginModel = new LoginModel(clientModelStore);

            LoginCommand = new LoginCommand(this, navigationService, loginModel);

        }
    }
}
