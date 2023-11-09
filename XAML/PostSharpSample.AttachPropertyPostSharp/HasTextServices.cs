using PostSharp.Patterns.Xaml;
using System.Windows;
using System.Windows.Controls;

namespace PostSharpSample.AttachPropertyPostSharp
{
    public class HasTextServices : DependencyObject
    {        
        [AttachedProperty]
        public static Attached<bool> IsEnabled { get; set; }
        public static void SetIsEnabled(DependencyObject host, bool value) => IsEnabled.SetValue(host, value);


        [AttachedProperty]
        public static Attached<bool> HasText { get; set; }
        public static void SetHasText(DependencyObject host, bool value) => HasText.SetValue(host, value);
    }
}