using System.Windows;
using System.Windows.Controls;

namespace PostSharpSample.DPProperty
{
    /// <summary>
    /// UserControl2.xaml 的互動邏輯
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public static readonly DependencyProperty SetTextProperty = UserControl1.SetTextProperty.AddOwner(typeof(UserControl2),
                  new FrameworkPropertyMetadata("",
                          FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnSetTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(SetTextProperty); }
            set { SetValue(SetTextProperty, value); }
        }

        public UserControl2()
        {
            InitializeComponent();
        }

        private static void OnSetTextChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            UserControl2 UserControl2Control = d as UserControl2;
            UserControl2Control.OnSetTextChanged(e);
        }

        private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
        {
            textBox1.Text = e.NewValue.ToString();
        }
    }
}