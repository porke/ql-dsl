using System;
using System.Diagnostics;
using System.Windows.Input;

namespace QL.Presentation
{
    public class RelayCommand<T> : ICommand where T: class
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null)
        {

        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(T parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(T parameter)
        {
            _execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute(parameter as T);
        }

        public void Execute(object parameter)
        {
            Execute(parameter as T);
        }
    }
}
