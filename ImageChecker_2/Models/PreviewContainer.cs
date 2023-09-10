using Prism.Mvvm;

namespace ImageChecker_2.Models
{
    public class PreviewContainer : BindableBase
    {
        private double scale = 1.0;
        private ImageFile imageFileA;
        private ImageFile imageFileB;
        private ImageFile imageFileC;
        private ImageFile imageFileD;

        public double X { get; set; }
        
        public double ActualX { get; set; }
        
        public double Y { get; set; }
        
        public double ActualY { get; set; }

        public double Scale
        {
            get => scale;
            set
            {
                if (SetProperty(ref scale, value))
                {
                    RaisePropertyChanged(nameof(ActualScale));
                }
            }
        }

        public double ActualScale => Scale / 4;

        public double Width { get; set; }
        
        public double Height { get; set; }

        public ImageFile ImageFileA { get => imageFileA; set => SetProperty(ref imageFileA, value); }

        public ImageFile ImageFileB { get => imageFileB; set => SetProperty(ref imageFileB, value); }

        public ImageFile ImageFileC { get => imageFileC; set => SetProperty(ref imageFileC, value); }

        public ImageFile ImageFileD { get => imageFileD; set => SetProperty(ref imageFileD, value); }

        public void SetImageFiles(ImageFile imageA, ImageFile imageB, ImageFile imageC, ImageFile imageD)
        {
            ImageFileA = imageA;
            ImageFileB = imageB;
            ImageFileC = imageC;
            ImageFileD = imageD;
        }
    }
}