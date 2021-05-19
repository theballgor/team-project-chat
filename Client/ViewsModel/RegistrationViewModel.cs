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

namespace Client.ViewsModel
{
    public class RegistrationViewModel: ViewModelBase
    { 

    }


    //partial class DataManageVM : INotifyPropertyChanged
    //{
    //    private string verifyPassword;

    //    public string VerifyPassword
    //    {
    //        get
    //        {
    //            return verifyPassword;
    //        }
    //        set
    //        {
    //            verifyPassword = value;
    //            OnPropertyChanged("VerifyPassword");
    //        }
    //    }

    //    public RelayCommand Register
    //    {
    //        get
    //        {
    //            return new RelayCommand(obj =>
    //            {
    //                try
    //                {
    //                    ClientServerMessage message = RegistrationModel.Handle(username, password, verifyPassword, email);
    //                    client.SendMessage(message);
    //                }
    //                catch (ArgumentException exc)
    //                {
    //                    MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
    //                }
    //            });
    //        }
    //    }

    //}
}
