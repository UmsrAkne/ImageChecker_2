using System.Windows;
using Prism.Mvvm;

namespace ImageChecker_2.Models
{
    public class PreviewContainer : BindableBase
    {
        private double scale = 1.0;
        private double x;
        private ImageFile imageFileA;
        private ImageFile imageFileB;
        private ImageFile imageFileC;
        private ImageFile imageFileD;
        private double y;
        private Rect slideRange;

        public double X
        {
            get => x;
            set
            {
                if (SetProperty(ref x, value))
                {
                    RaisePropertyChanged(nameof(ActualX));
                }
            }
        }

        public double ActualX => X * PreviewScreenScale;

        public double Y
        {
            get => y;
            set
            {
                if (SetProperty(ref y, value))
                {
                    RaisePropertyChanged(nameof(ActualY));
                }
            }
        }

        public double ActualY => Y * PreviewScreenScale;

        public double Scale
        {
            get => scale;
            set
            {
                if (!SetProperty(ref scale, value))
                {
                    return;
                }

                RaisePropertyChanged(nameof(ActualScale));
                UpdateSlideRange();
            }
        }

        public double ActualScale => Scale * PreviewScreenScale;

        public double Width { get; private set; } = 320;

        public double Height { get; private set; } = 180;

        public double ScreenWidth => Width / PreviewScreenScale;
        
        public double ScreenHeight => Height / PreviewScreenScale;

        public Rect SlideRange { get => slideRange; private set => SetProperty(ref slideRange, value); }

        public ImageFile ImageFileA
        {
            get => imageFileA;
            set
            {
                SetProperty(ref imageFileA, value);
                UpdateSlideRange();
            }
        }

        public ImageFile ImageFileB { get => imageFileB; set => SetProperty(ref imageFileB, value); }

        public ImageFile ImageFileC { get => imageFileC; set => SetProperty(ref imageFileC, value); }

        public ImageFile ImageFileD { get => imageFileD; set => SetProperty(ref imageFileD, value); }
        
        private double PreviewScreenScale { get; set; } = 0.25;

        private void UpdateSlideRange()
        {
            if (ImageFileA == null)
            {
                SlideRange = new Rect(-1280, -720, 2560, 1440);
                return;
            }

            SlideRange = new Rect(
                ImageFileA.Width * Scale * -1,
                ImageFileA.Height * Scale * -1,
                ImageFileA.Width * 2.0 * Scale, 
                ImageFileA.Height * 2.0 * Scale);
        }
    }
}