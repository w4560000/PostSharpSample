using System.ComponentModel;

namespace PostSharpSample.WPF.MVVM
{
    public class PostsViewModel : INotifyPropertyChanged
    {
        public Posts Posts { get; set; } = new Posts { PostsTitle1 = "postsTitle1Unknown", PostsTitle2 = "postsTitle2Unknown" };

        public event PropertyChangedEventHandler PropertyChanged;

        public string ModelPostsTitle1
        {
            get => Posts.PostsTitle1;
            set
            {
                Posts.PostsTitle1 = value;
                OnPropertyChanged(nameof(ModelPostsTitle1));
            }
        }

        public string ModelPostsTitle2
        {
            get { return Posts.PostsTitle2; }
            set
            {
                Posts.PostsTitle2 = value;
                OnPropertyChanged(nameof(ModelPostsTitle2));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}