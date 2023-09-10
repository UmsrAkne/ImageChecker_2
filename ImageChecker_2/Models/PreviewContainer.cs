using Prism.Mvvm;

namespace ImageChecker_2.Models
{
    public class PreviewContainer : BindableBase
    {
        private double scale;

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
        
        public ImageFile ImageFileA { get; set; }
        
        public ImageFile ImageFileB { get; set; }
        
        public ImageFile ImageFileC { get; set; }
        
        public ImageFile ImageFileD { get; set; }
        
        public void SetImageFiles(ImageFile imageA, ImageFile imageB, ImageFile imageC, ImageFile imageD)
        {
            ImageFileA = imageA;
            ImageFileB = imageB;
            ImageFileC = imageC;
            ImageFileD = imageD;
        }
    }
}