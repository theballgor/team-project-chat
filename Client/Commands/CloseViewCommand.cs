using Client.Services;
using Client.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    class CloseViewCommand: CommandBase
    {
        private readonly RegistrationViewModel _viewModel;
        private readonly NavigationService<LoginViewModel> _navigationService;

        public CloseViewCommand(RegistrationViewModel viewModel, NavigationService<LoginViewModel> navigationService)
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
