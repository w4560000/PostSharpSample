using System.Windows.Input;

namespace PostSharpSample.Command
{
    public class MainViewModel
    {
        private ICommand ShowCommand = new ShowCommand();

        public ICommand Show
        {
            get { return ShowCommand; }
        }
    }
}