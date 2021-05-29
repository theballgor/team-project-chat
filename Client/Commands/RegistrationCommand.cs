using Client.Services;
using Client.Store;
using Client.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    class RegistrationCommand: CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly NavigationService<RegistrationViewModel> _navigationService;

        public RegistrationCommand(LoginViewModel viewModel, NavigationService<RegistrationViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
