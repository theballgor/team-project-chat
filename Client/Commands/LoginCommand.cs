using Client.Model;
using Client.Services;
using Client.ViewsModel;
using ClientLibrary;
using ClientServerLibrary;


namespace Client.Commands
{
    class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly NavigationService<AccountViewModel> _navigationService;

        public LoginCommand(LoginViewModel viewModel, NavigationService<AccountViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            LoginModel.TryLogin();
        }
    }
}
