using System.ComponentModel;

namespace PostSharpSample.WPF.MVVM
{
    public class Posts : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string postTitle1 { get; set; }

        public string PostsTitle1
        {
            get { return postTitle1; }
            set
            {
                if (postTitle1 != value)
                {
                    postTitle1 = value;
                    OnPropertyChanged(nameof(PostsTitle1));
                }
            }
        }

        private string postTitle2 { get; set; }

        public string PostsTitle2
        {
            get { return postTitle2; }
            set
            {
                if (postTitle2 != value)
                {
                    postTitle2 = value;
                    OnPropertyChanged(nameof(PostsTitle2));
                }
            }
        }
    }
}