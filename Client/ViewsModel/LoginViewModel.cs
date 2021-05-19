using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.Commands;
using System.Windows;
using System.Windows.Input;
using Client.Services;
using Client.Store;
using Client.Stores;
using Client.Model;
using ClientServerLibrary.DbClasses;

namespace Client.ViewsModel
{
    partial class DataManageVM : INotifyPropertyChanged
    {
       
        public RelayCommand Login
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    try
                    {
                        Validate();
                        Send();
                    }
                    catch (ArgumentException exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        public RelayCommand RegistrationWindow
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    try
                    {
                        //open reg window 
                        

                    }
                    catch (ArgumentException exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        private void Send()
        {
            MessageBox.Show("Ok");
        }

        private void Validate()
        {
            ValidateString(password, "Invalid data", 8, 16);
            ValidateEmail("Invalid data");
        }

        private void ValidateString(string str, string exceptionMessage, int from, int to)
        {
            if (string.IsNullOrEmpty(str) || (str.Length < from || str.Length > to))
                throw new ArgumentException(exceptionMessage);
        }

        private void ValidateEmail(string exceptionMessage)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}
