using Client.Commands;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewsModel
{
    /// <summary>
    /// MVVM
    /// </summary>
    class DataManageVM
    {
        //To pin chat om Pin Button Click

        protected ICommand _openMenuCommand;
        public ICommand OpenMenuCommand
        {
            get
            {
                return _openMenuCommand ??
                    (_openMenuCommand = new RelayCommand(parameter =>
                    {
                        //if (parameter is CommandAssist v)
                        //{
                        //    v.MenuIsOppened = true;
                        //}
                    }));
            }
        } 
        
        protected ICommand _closeMenuCommand;
        public ICommand CloseMenuCommand
        {
            get
            {
                return _closeMenuCommand ??
                    (_closeMenuCommand = new RelayCommand(parameter =>
                    {
                        //if (parameter is CommandAssist v)
                        //{
                        //    v.MenuIsOppened = false;
                        //}
                    }));
            }
        }

    }
}
