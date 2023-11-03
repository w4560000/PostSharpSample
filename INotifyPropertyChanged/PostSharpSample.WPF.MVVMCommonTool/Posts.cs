using CommunityToolkit.Mvvm.ComponentModel;

namespace PostSharpSample.WPF.MVVMCommonTool
{
    public class Posts : ObservableObject
    {
        private string postTitle1;

        public string PostsTitle1
        {
            get => postTitle1;
            set => SetProperty(ref postTitle1, value);
        }

        private string postTitle2;

        public string PostsTitle2
        {
            get => postTitle2;
            set => SetProperty(ref postTitle2, value);
        }
    }
}