using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ImageChecker_2.Models;
using ImageChecker_2.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Point = System.Drawing.Point;

namespace ImageChecker_2.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private string title = "Image checker ver 2";
        private ImageContainer imageContainerA;
        private ImageContainer imageContainerB;
        private ImageContainer imageContainerC;
        private ImageContainer imageContainerD;

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

        public PreviewContainer PreviewContainer { get; } = new PreviewContainer();

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

        public ObservableCollection<History> Histories { get; private set; } = new ObservableCollection<History>();

        public Visibility PreviewAreaVisibility { get; private set; } = Visibility.Visible;

        public DelegateCommand ShowSettingPageCommand => new DelegateCommand(() =>
        {
            dialogService.ShowDialog(nameof(SettingPage), new DialogParameters(), _ =>
            {
            });
        });

        public DelegateCommand<object> GenerateTagCommand => new DelegateCommand<object>((param) =>
        {
            if (!(param is KeyBinding kb))
            {
                return;
            }

            var setting = Setting.Read("setting.xml");
            var tagText = string.Empty;

            switch (kb.Key)
            {
                case Key.D when (kb.Modifiers & ModifierKeys.Control) > 0 && (kb.Modifiers & ModifierKeys.Shift) > 0:
                    tagText = TagGenerator.GetTag(setting.AnimationDrawTagBaseText, PreviewContainer);
                    break;
                case Key.D when (kb.Modifiers & ModifierKeys.Control) > 0:
                    tagText = TagGenerator.GetTag(setting.DrawTagBaseText, PreviewContainer);
                    break;
                case Key.I when (kb.Modifiers & ModifierKeys.Control) > 0:
                    tagText = TagGenerator.GetTag(setting.ImageTagBaseText, PreviewContainer);
                    break;
            }

            if (!string.IsNullOrEmpty(tagText))
            {
                Clipboard.SetData(DataFormats.Text, tagText);
                var h = new History()
                {
                    TagText = tagText,
                    Scale = PreviewContainer.Scale,
                    Pos = new Point((int)PreviewContainer.X, (int)PreviewContainer.Y),
                    DisplayPos = new Point((int)PreviewContainer.DisplayX, (int)PreviewContainer.DisplayY),
                    ImageFileA = ImageContainerA?.CurrentFile,
                    ImageFileB = ImageContainerB?.CurrentFile,
                    ImageFileC = ImageContainerC?.CurrentFile,
                    ImageFileD = ImageContainerD?.CurrentFile,
                };

                Histories.Add(h);
            }
        });
        
        public DelegateCommand<History> RestoreHistoryCommand => new DelegateCommand<History>(h =>
        {
            if (h == null)
            {
                return;
            }

            ImageContainerA.CurrentFile = h.ImageFileA;
            ImageContainerB.CurrentFile = h.ImageFileB;
            ImageContainerC.CurrentFile = h.ImageFileC;
            ImageContainerD.CurrentFile = h.ImageFileD;

            PreviewContainer.Scale = h.Scale;
            PreviewContainer.X = h.Pos.X;
            PreviewContainer.Y = h.Pos.Y;
        });

        public DelegateCommand<History> RestorePreviewStatusCommand => new DelegateCommand<History>(h =>
        {
            if (h == null)
            {
                return;
            }

            PreviewContainer.Scale = h.Scale;
            PreviewContainer.X = h.Pos.X;
            PreviewContainer.Y = h.Pos.Y;
        });

        public DelegateCommand<object> ChangePreviewScaleCommand => new DelegateCommand<object>((param) =>
        {
            var scale = (ZoomScale)param;

            switch (scale)
            {
                case ZoomScale.Low:
                    PreviewContainer.Width = 320;
                    PreviewContainer.Height = 180;
                    PreviewContainer.PreviewScreenScale = 0.25;
                    break;
                case ZoomScale.Middle:
                    PreviewContainer.Width = 640;
                    PreviewContainer.Height = 360;
                    PreviewContainer.PreviewScreenScale = 0.5;
                    break;
                case ZoomScale.High:
                    break;
            }
        });

        public DelegateCommand ChangePreviewAreaVisibilityCommand => new DelegateCommand(() =>
        {
            PreviewAreaVisibility = PreviewAreaVisibility == Visibility.Visible 
                ? Visibility.Collapsed 
                : Visibility.Visible;
            
            RaisePropertyChanged(nameof(PreviewAreaVisibility));
        });

        public DelegateCommand<object> ChangeScreenSizeCommand => new DelegateCommand<object>((rect) =>
        {
            var r = (Rect)rect;
            PreviewContainer.Width = r.Width;
            PreviewContainer.Height = r.Height;
        });

        public DelegateCommand<ImageFile> SelectableImageFilterCommand => new DelegateCommand<ImageFile>((imageFile) =>
        {
            if (imageFile == null || !imageFile.IsMatchingNamingRule)
            {
                return;
            }

            if (!imageFile.FileInfo.Name.Contains('A'))
            {
                return;
            }

            ImageContainerB.SelectSameGroupImages(imageFile);
            ImageContainerC.SelectSameGroupImages(imageFile);
            ImageContainerD.SelectSameGroupImages(imageFile);
        });

        public DelegateCommand<object> MoveImageCommand => new DelegateCommand<object>((pt) =>
        {
            var p = new Point((int)((System.Windows.Point)pt).X, (int)((System.Windows.Point)pt).Y);
            if (p.X != 0)
            {
                PreviewContainer.X += 20 * p.X;
                return;
            }

            if (p.Y != 0)
            {
                PreviewContainer.Y -= 20 * p.Y;
            }
        });

        public void LoadImages(IEnumerable<string> filePaths)
        {
            ImageContainerA = new ImageContainer("A");
            ImageContainerB = new ImageContainer("B");
            ImageContainerC = new ImageContainer("C");
            ImageContainerD = new ImageContainer("D");

            ImageContainerA.CurrentFileChanged += (sender, _) =>
                PreviewContainer.ImageFileA = ((ImageContainer)sender)?.CurrentFile;

            ImageContainerB.CurrentFileChanged += (sender, _) =>
                PreviewContainer.ImageFileB = ((ImageContainer)sender)?.CurrentFile;

            ImageContainerC.CurrentFileChanged += (sender, _) =>
                PreviewContainer.ImageFileC = ((ImageContainer)sender)?.CurrentFile;

            ImageContainerD.CurrentFileChanged += (sender, _) =>
                PreviewContainer.ImageFileD = ((ImageContainer)sender)?.CurrentFile;

            var fs = filePaths.ToList();
            new List<ImageContainer> { ImageContainerA, ImageContainerB, ImageContainerC, ImageContainerD, }
                .ForEach(c => c.Load(fs));
        }
    }
}