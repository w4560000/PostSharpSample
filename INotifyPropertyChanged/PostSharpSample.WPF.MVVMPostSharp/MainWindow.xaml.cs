using System.Windows;

namespace PostSharpSample.WPF.MVVMPostSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PostsViewModel _postsViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _postsViewModel = (PostsViewModel)base.DataContext;
        }
    }
}