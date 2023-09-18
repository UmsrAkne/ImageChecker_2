using System.Windows;
using Prism.Commands;
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
        private double width = 320;
        private double height = 180;
        private double previewScreenScale = 0.25;

        public double X
        {
            get => x;
            set
            {
                if (SetProperty(ref x, value))
                {
                    RaisePropertyChanged(nameof(ActualX));
                    RaisePropertyChanged(nameof(DisplayX));
                }
            }
        }

        public double DisplayX => (ImageWidth * Scale / 2) + X - (Width / PreviewScreenScale / 2);

        public double ActualX => X * PreviewScreenScale;

        public double Y
        {
            get => y;
            set
            {
                if (SetProperty(ref y, value))
                {
                    RaisePropertyChanged(nameof(ActualY));
                    RaisePropertyChanged(nameof(DisplayY));
                }
            }
        }

        public double ActualY => Y * PreviewScreenScale;
        
        public double DisplayY => (ImageHeight * Scale / 2) + Y - (Height / PreviewScreenScale / 2);

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
                RaisePropertyChanged(nameof(DisplayX));
                RaisePropertyChanged(nameof(DisplayY));
                UpdateSlideRange();
            }
        }

        public double ActualScale => Scale * PreviewScreenScale;

        public double Width
        {
            get => width;
            set
            {
                if (SetProperty(ref width, value))
                {
                    RaisePropertyChanged(nameof(DisplayX));
                    RaisePropertyChanged(nameof(ScreenWidth));
                }
            }
        }

        public double Height
        {
            get => height;
            set
            {
                if (SetProperty(ref height, value))
                {
                    RaisePropertyChanged(nameof(DisplayY));
                    RaisePropertyChanged(nameof(ScreenHeight));
                }
            }
        }

        public double ScreenWidth => Width / PreviewScreenScale;
        
        public double ScreenHeight => Height / PreviewScreenScale;

        public Rect SlideRange { get => slideRange; private set => SetProperty(ref slideRange, value); }

        public ImageFile ImageFileA
        {
            get => imageFileA;
            set
            {
                var oldIndex = imageFileA?.Index ?? -1;
                var newIndex = value?.Index ?? -1;
                
                SetProperty(ref imageFileA, value);
                
                if (oldIndex != newIndex)
                {
                    // 新旧のメインインデックスが異なるということは、類似画像ではないため、ポジションをリセットする。
                    ResetPostAndScaleCommand.Execute();
                }
                
                UpdateSlideRange();
            }
        }

        public ImageFile ImageFileB { get => imageFileB; set => SetProperty(ref imageFileB, value); }

        public ImageFile ImageFileC { get => imageFileC; set => SetProperty(ref imageFileC, value); }

        public ImageFile ImageFileD { get => imageFileD; set => SetProperty(ref imageFileD, value); }
        
        public DelegateCommand<object> SetPositionCommand => new DelegateCommand<object>((param) =>
        {
            var p = (Point)param;
            if (p == new Point(0, 0))
            {
                SetCenter();
                return;
            }

            if (p.X != 0)
            {
                X = p.X > 0 ? ((ImageWidth * Scale) - ScreenWidth) * -1 : 0;
            }
            
            if (p.Y != 0)
            {
                Y = p.Y > 0 ? ((ImageHeight * Scale) - ScreenHeight) * -1 : 0;
            }
        });

        public DelegateCommand ResetPostAndScaleCommand => new DelegateCommand(() =>
        {
            Scale = 1.0;
            SetCenter();
        });

        public double PreviewScreenScale
        {
            get => previewScreenScale;
            set
            {
                if (SetProperty(ref previewScreenScale, value))
                {
                    RaisePropertyChanged(nameof(DisplayX));
                    RaisePropertyChanged(nameof(ActualX));
                    RaisePropertyChanged(nameof(ActualY));
                    RaisePropertyChanged(nameof(DisplayY));
                    RaisePropertyChanged(nameof(ActualScale));
                    RaisePropertyChanged(nameof(ScreenWidth));
                    RaisePropertyChanged(nameof(ScreenHeight));
                    RaisePropertyChanged(nameof(SetPositionCommand));
                }
            }
        }

        private double ImageWidth => ImageFileA?.Width ?? 0;
        
        private double ImageHeight => ImageFileA?.Height ?? 0;

        public void SetCenter()
        {
            X = -((ImageWidth * Scale) - ScreenWidth) / 2;
            Y = -((ImageHeight * Scale) - ScreenHeight) / 2;
        }
        
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