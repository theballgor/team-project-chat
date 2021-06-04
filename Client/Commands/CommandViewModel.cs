using System;

using System.Windows.Input;

namespace Client.Commands
{
    class CommandViewModel: ICommand
    {
        private readonly Action _action;
        public CommandViewModel(Action action)
        {
            _action = action;
        }

        // For save logick commands
        public void Execute(object o)
        {
            _action();
        }

        /// <summary>
        /// Return true if command is ON each false 
        /// </summary>
        public bool CanExecute(object o)
        {
            return true;
        }
        /// <summary>
        /// Call when the state of the commands changes
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
