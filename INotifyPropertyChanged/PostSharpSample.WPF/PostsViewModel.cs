using System.ComponentModel;

namespace PostSharpSample.WPF
{
    public class PostsViewModel : INotifyPropertyChanged
    {
        public Posts posts { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PostsViewModel()
        {
            posts = new Posts { postsText = "", postsTitle1 = "postsTitle1Unknown", postsTitle2 = "postsTitle2Unknown" };
        }

        public string PostsTitle1
        {
            get { return posts.postsTitle1; }
            set
            {
                if (posts.postsTitle1 != value)
                {
                    posts.postsTitle1 = value;
                    RaisePropertyChanged(nameof(Posts.postsTitle1));
                }
            }
        }

        public string PostsTitle2
        {
            get { return posts.postsTitle2; }
            set
            {
                if (posts.postsTitle2 != value)
                {
                    posts.postsTitle2 = value;
                    RaisePropertyChanged(nameof(Posts.postsTitle2));
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}