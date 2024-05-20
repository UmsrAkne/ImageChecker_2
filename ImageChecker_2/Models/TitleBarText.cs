using Prism.Mvvm;

namespace ImageChecker_2.Models
{
    public class TitleBarText : BindableBase
    {
        private string title;
        private string version = string.Empty;

        public string Title
        {
            get => string.IsNullOrWhiteSpace(Version)
                ? title
                : title + " version : " + Version;

            set => SetProperty(ref title, value);
        }

        public string Version { get => version; set => SetProperty(ref version, value); }
    }
}