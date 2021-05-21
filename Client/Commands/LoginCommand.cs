
using Client.Model;
using Client.Services;
using Client.Stores;
using Client.ViewsModel;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Client.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly LoginModel _loginModel;
        private readonly UserStore _userStore;
        private readonly NavigationService<AccountViewModel> _navigationService;

        public LoginCommand(LoginViewModel loginViewModel, NavigationService<AccountViewModel> navigationService, LoginModel loginModel)
        {
            DataWorker.ReceivingDataEvent += DataWorker_ReceivingDataEvent;
            _loginViewModel = loginViewModel;
            _loginModel = loginModel;
            _navigationService = navigationService;

        }

        public override void Execute(object parameter)
        {

            _loginModel.TryLogin( _loginViewModel.Username, _loginViewModel.Password);
            Thread.Sleep(5000);



  
            //ClientModel.CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetFreeTcpPort());
            //ClientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);

            //client = new ClientModel(new IPEndPoint(GlobalVariables.LocalIP, 5001));
            //client.Connect(new IPEndPoint(GlobalVariables.LocalIP, GlobalVariables.ServerPort));
            //client.StartListening();



            //_navigationService.Navigate();

        }
            void DataWorker_ReceivingDataEvent(object sender, DataWorker.ReceivingDataEcentArgs e)
            {
                User user = (User)e.Data;

                if(user!=null)
                    _navigationService.Navigate();
            }

    }
}
