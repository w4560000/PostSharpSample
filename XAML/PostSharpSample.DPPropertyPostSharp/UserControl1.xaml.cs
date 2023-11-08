using PostSharp.Patterns.Model;
using PostSharp.Patterns.Xaml;
using System.Windows;
using System.Windows.Controls;

namespace PostSharpSample.DPPropertyPostSharp
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    [NotifyPropertyChanged]
    public partial class UserControl1 : UserControl
    {

        //public static DependencyProperty SetTextProperty { get; set; }

        public UserControl1()
        {
            //SetText = "777";
            InitializeComponent();
            var a = SetText;
        }

        [DependencyProperty]
        public string SetText { get; set; }

        private void OnSetTextChanged()
        {
            textBox.Text = SetText;
        }
    }
}