using PostSharp.Patterns.Model;

namespace PostSharpSample.WPF.MVVMPostSharp
{
    [NotifyPropertyChanged]
    public class PostsViewModel
    {
        public Posts Posts { get; set; } = new Posts { PostsTitle1 = "TestPostsTitle1", PostsTitle2 = "TestPostsTitle2" };

        public string ModelPostsTitle1
        {
            get => Posts.PostsTitle1;
            set => Posts.PostsTitle1 = value;
        }

        // IgnoreAutoChangeNotification 可以忽略自動更新此 Property
        // [IgnoreAutoChangeNotification]
        public string ModelPostsTitle2
        {
            get => Posts.PostsTitle2;
            set => Posts.PostsTitle2 = value;
        }
    }
}