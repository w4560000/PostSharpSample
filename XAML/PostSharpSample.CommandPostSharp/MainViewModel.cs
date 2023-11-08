using PostSharp.Patterns.Xaml;
using System.Windows;
using System.Windows.Input;

namespace PostSharpSample.CommandPostSharp
{
    public class MainViewModel
    {
        [Command(ExecuteMethod = nameof(ExecuteShow), CanExecuteMethod = nameof(CanShow))]
        public ICommand Show { get; private set; }

        public bool CanShow(object parameter)
        {
            return true;
        }

        public void ExecuteShow(object parameter)
        {
            if (parameter != null)
                MessageBox.Show(parameter.ToString());
        }
    }
}