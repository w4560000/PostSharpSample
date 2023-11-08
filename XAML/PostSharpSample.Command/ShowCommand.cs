using System;
using System.Windows;
using System.Windows.Input;

namespace PostSharpSample.Command
{
    public class ShowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
                MessageBox.Show(parameter.ToString());
        }
    }
}