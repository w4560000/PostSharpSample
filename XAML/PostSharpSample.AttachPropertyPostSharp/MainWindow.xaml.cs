using System.Windows;
using System.Windows.Controls;

namespace PostSharpSample.AttachPropertyPostSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            HasTextServices.SetHasText(tb, !string.IsNullOrEmpty(tb.Text));
            //HasTextServices.SetDock(tb, Dock.Top);
        }
    }
}