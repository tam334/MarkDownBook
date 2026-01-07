using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MarkDownBookLauncher.ViewModel
{
    internal class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        event EventHandler? ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        bool ICommand.CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        void ICommand.Execute(object? parameter)
        {
            _execute();
        }
    }
}
