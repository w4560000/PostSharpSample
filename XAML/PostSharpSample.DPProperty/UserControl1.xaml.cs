using System.Windows;
using System.Windows.Controls;

namespace PostSharpSample.DPProperty
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SetTextProperty =
         DependencyProperty.Register("SetText", typeof(string), typeof(UserControl1), new FrameworkPropertyMetadata("",
            FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnSetTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(SetTextProperty); }
            set { SetValue(SetTextProperty, value); }
        }

        private static void OnSetTextChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            UserControl1 UserControl1Control = d as UserControl1;
            UserControl1Control.OnSetTextChanged(e);
        }

        private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
        {
            textBox.Text = e.NewValue.ToString();
        }
    }
}