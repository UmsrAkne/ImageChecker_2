using System.Collections.Generic;
using System.Linq;
using ImageChecker_2.Models;
using Prism.Mvvm;

namespace ImageChecker_2.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Image checker ver 2";
        private ImageContainer imageContainerA;
        private ImageContainer imageContainerB;
        private ImageContainer imageContainerC;
        private ImageContainer imageContainerD;

        public MainWindowViewModel()
        {
        }
        
        public string Title { get => title; set => SetProperty(ref title, value); }
        
        public ImageContainer ImageContainerA
        {
            get => imageContainerA;
            private set => SetProperty(ref imageContainerA, value);
        }

        public ImageContainer ImageContainerB
        {
            get => imageContainerB;
            private set => SetProperty(ref imageContainerB, value);
        }

        public ImageContainer ImageContainerC
        {
            get => imageContainerC;
            private set => SetProperty(ref imageContainerC, value);
        }

        public ImageContainer ImageContainerD
        {
            get => imageContainerD;
            private set => SetProperty(ref imageContainerD, value);
        }

        public void LoadImages(IEnumerable<string> filePaths)
        {
            ImageContainerA = new ImageContainer("A");
            ImageContainerB = new ImageContainer("B");
            ImageContainerC = new ImageContainer("C");
            ImageContainerD = new ImageContainer("D");

            var fs = filePaths.ToList();
            new List<ImageContainer> { ImageContainerA, ImageContainerB, ImageContainerC, ImageContainerD, }
                .ForEach(c => c.Load(fs));
        }
    }
}