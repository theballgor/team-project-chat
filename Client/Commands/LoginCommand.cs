using Client.Model;
using Client.Services;
using Client.Store;
using Client.Stores;
using Client.ViewsModel;
using ClientServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Client.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly ClientModelStore _clientModelStore;
        private readonly NavigationService<AccountViewModel> _navigationService;

        public LoginCommand(LoginViewModel viewModel, ClientModelStore clientModelStore, NavigationService<AccountViewModel> navigationService)
        {
            _viewModel = viewModel;
            _clientModelStore = clientModelStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            ClientModel client;
            client = new ClientModel(new IPEndPoint(GlobalVariables.LocalIP, 5001));
            client.Connect(new IPEndPoint(GlobalVariables.LocalIP, GlobalVariables.ServerPort));
            client.StartListening();

            _clientModelStore.CurrentClientModel = client;

            _navigationService.Navigate();

        }
    }
}
