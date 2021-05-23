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

namespace Client.ViewsModel
{
    class RegistrationViewModel : ViewModelBase
    {
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

        private readonly NavigationStore navigationStore;
        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;
        NavigationService<LoginViewModel> navigationService;

        // Constructor
        public RegistrationViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            RegistrationModel.RegisterSucces += RegistrationModel_RegistrationSucces;
            navigationService = new NavigationService<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));
        }

        // Commands
        //public RelayCommand Register
        //{
        //    get
        //    {
        //        return new RelayCommand(obj =>
        //        {
        //            try
        //            {
        //                RegistrationModel.TryRegister();
        //            }
        //            catch (ArgumentException exc)
        //            {
        //                MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        });
        //    }
        //}

        // Callback
        private void RegistrationModel_RegistrationSucces(object sender, EventArgs e)
        {
            if ((e as ViewModelEventArgs).Content == null) throw new ArgumentNullException("failed to register");
            switch ((RegistrationResult)(e as ViewModelEventArgs).Content)
            {
                case RegistrationResult.Success:
                    Console.WriteLine("user succesfylly refistered");
                    navigationService.Navigate();
                    break;

                case RegistrationResult.UserNameAlreadyExists:
                    Console.WriteLine("user already exist");
                    break;

                case RegistrationResult.EmailAlreadyExists:
                    Console.WriteLine("email already exist");
                    break;

                default:
                    Console.WriteLine("failed to register");
                    break;

            }
        }
        protected virtual void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
