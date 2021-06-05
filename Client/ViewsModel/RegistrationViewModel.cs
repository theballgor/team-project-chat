using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.Commands;
using System.Windows;
using Client.Model;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using ClientLibrary;
using Client.Store;
using Client.Services;
using System.Windows.Input;

namespace Client.ViewsModel
{
    class RegistrationViewModel : ViewModelBase
    {
        private readonly NavigationService<LoginViewModel> navigationService;

        // Fields
        public string Email
        {
            get
            {
                return RegistrationModel.Email;
            }
            set
            {
                RegistrationModel.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get
            {
                return RegistrationModel.Password;
            }
            set
            {
                RegistrationModel.Password = value;
                OnPropertyChanged("Password");
            }
        }
        public string Username
        {
            get
            {
                return RegistrationModel.Username;
            }
            set
            {
                RegistrationModel.Username = value;
                OnPropertyChanged("Username");
            }
        }
        public string VerifyPassword
        {
            get
            {
                return RegistrationModel.VerifyPassword;
            }
            set
            {
                RegistrationModel.VerifyPassword = value;
                OnPropertyChanged("VerifyPassword");
            }
        }

        private ICommand _closeViewCommand;
        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand(parameter =>
                {
                    RegistrationModel.TryRegister();
                }));
            }
        }
        public ICommand CloseViewCommand
        {
            get
            {
                return _closeViewCommand ?? (_closeViewCommand = new RelayCommand(parameter =>
                {
                    navigationService.Navigate();
                }));
            }
        }

        // Constructor
        public RegistrationViewModel(NavigationStore navigationStore)
        {
            RegistrationModel.RegisterSucces += RegistrationModel_RegistrationSucces;
            navigationService = new NavigationService<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));
        }

        // Callback
        private void RegistrationModel_RegistrationSucces(object sender, EventArgs e)
        {
            switch ((RegistrationResult)(e as ViewModelEventArgs).Content)
            {
                case RegistrationResult.Success:
                    Console.WriteLine("Succes registration");
                    navigationService.Navigate();
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    break;
                case RegistrationResult.UserNameAlreadyExists:
                    break;
                case RegistrationResult.PhoneNumberAlreadyExists:
                    break;
                case RegistrationResult.CreationError:
                    break;
                default:
                    break;
            }
        }
    }
}