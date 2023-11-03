using PostSharp.Patterns.Model;

namespace PostSharpSample.WPF.MVVMPostSharp
{
    [NotifyPropertyChanged]
    public class Posts
    {
        public string PostsTitle1 { get; set; }

        public string PostsTitle2 { get; set; }
    }
}