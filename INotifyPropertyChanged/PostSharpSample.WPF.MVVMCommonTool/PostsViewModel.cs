using CommunityToolkit.Mvvm.ComponentModel;

namespace PostSharpSample.WPF.MVVMCommonTool
{
    public class PostsViewModel : ObservableObject
    {
        public Posts Posts { get; set; } = new Posts { PostsTitle1 = "TestPostsTitle1", PostsTitle2 = "TestPostsTitle2" };

        public string ModelPostsTitle1
        {
            get => Posts.PostsTitle1;
            set => SetProperty(Posts.PostsTitle1, value, Posts, (u, n) => u.PostsTitle1 = n);
        }

        public string ModelPostsTitle2
        {
            get => Posts.PostsTitle2;
            set => SetProperty(Posts.PostsTitle2, value, Posts, (u, n) => u.PostsTitle2 = n);
        }
    }
}