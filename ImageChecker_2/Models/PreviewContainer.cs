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

        public double ActualX => X / 4;

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

        public double ActualY => Y / 4;

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

        public double Width { get; private set; } = 320;

        public double Height { get; private set; } = 180;

        public ImageFile ImageFileA { get => imageFileA; set => SetProperty(ref imageFileA, value); }

        public ImageFile ImageFileB { get => imageFileB; set => SetProperty(ref imageFileB, value); }

        public ImageFile ImageFileC { get => imageFileC; set => SetProperty(ref imageFileC, value); }

        public ImageFile ImageFileD { get => imageFileD; set => SetProperty(ref imageFileD, value); }
    }
}