using Client.Model;
using Client.Services;
using Client.ViewsModel;


namespace Client.Commands
{
    class RegisterCommand: CommandBase
    {
        private readonly RegistrationViewModel _viewModel;
        private readonly NavigationService<LoginViewModel> _navigationService;

        public RegisterCommand(RegistrationViewModel viewModel, NavigationService<LoginViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            RegistrationModel.TryRegister();
        }
    }
}
