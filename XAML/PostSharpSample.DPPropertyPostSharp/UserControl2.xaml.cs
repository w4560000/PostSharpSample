using PostSharp.Patterns.Model;
using PostSharp.Patterns.Xaml;
using System.Windows;
using System.Windows.Controls;

namespace PostSharpSample.DPPropertyPostSharp
{
    /// <summary>
    /// UserControl2.xaml 的互動邏輯
    /// Fail 沒有測試出來，SetText 繼承不到父組件的值
    /// </summary>
    [NotifyPropertyChanged]
    public partial class UserControl2 : UserControl
    {

        public UserControl2()
        {
            InitializeComponent();

            var a = SetText;
        }

        public static readonly DependencyProperty SetTextProperty = DependencyPropertyServices.GetDependencyProperty(typeof(UserControl1), nameof(UserControl1.SetText)).AddOwner(typeof(UserControl2),
          new FrameworkPropertyMetadata("",
                  FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnSetTextChanged)));

        [SafeForDependencyAnalysis]
        public string SetText
        {
            get {
                var a = GetValue(SetTextProperty);
                return (string)GetValue(SetTextProperty);
            }
            set { SetValue(SetTextProperty, value); }
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