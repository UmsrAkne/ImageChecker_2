using Prism.Mvvm;

namespace ImageChecker_2.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Image checker ver 2";

        public MainWindowViewModel()
        {
        }
        
        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}