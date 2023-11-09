using System.Windows;
using System.Windows.Controls;

namespace PostSharpSample.AttachProperty
{
    public class HasTextServices
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(HasTextServices),
            new PropertyMetadata(default(bool), OnIsEnabledChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsEnabled(DependencyObject obj)
            => (bool)obj.GetValue(IsEnabledProperty);

        public static void SetIsEnabled(DependencyObject obj, bool value)
            => obj.SetValue(IsEnabledProperty, value);

        public static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached(
            "HasText",
            typeof(bool),
            typeof(HasTextServices),
            new PropertyMetadata(default(bool)));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetHasText(DependencyObject obj)
            => (bool)obj.GetValue(HasTextProperty);

        public static void SetHasText(DependencyObject obj, bool value)
            => obj.SetValue(HasTextProperty, value);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var tb = d as TextBox;
            //if ((bool)e.NewValue)
            //{
            //    tb.TextChanged += Tb_TextChanged;
            //}
            //else
            //{
            //    tb.TextChanged -= Tb_TextChanged;
            //}
        }

        private static void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}