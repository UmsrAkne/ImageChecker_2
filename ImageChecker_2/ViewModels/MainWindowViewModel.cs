﻿using System.Collections.Generic;
using System.Linq;
using ImageChecker_2.Models;
using ImageChecker_2.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

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

        public DelegateCommand ShowSettingPageCommand => new DelegateCommand(() =>
        {
            dialogService.ShowDialog(nameof(SettingPage), new DialogParameters(),  _ => { }); 
        });

        public void LoadImages(IEnumerable<string> filePaths)
        {
            ImageContainerA = new ImageContainer("A");
            ImageContainerB = new ImageContainer("B");
            ImageContainerC = new ImageContainer("C");
            ImageContainerD = new ImageContainer("D");

            ImageContainerA.CurrentFileChanged += (sender, _) => PreviewContainer.ImageFileA = ((ImageContainer)sender)?.CurrentFile;
            ImageContainerB.CurrentFileChanged += (sender, _) => PreviewContainer.ImageFileB = ((ImageContainer)sender)?.CurrentFile;
            ImageContainerC.CurrentFileChanged += (sender, _) => PreviewContainer.ImageFileC = ((ImageContainer)sender)?.CurrentFile;
            ImageContainerD.CurrentFileChanged += (sender, _) => PreviewContainer.ImageFileD = ((ImageContainer)sender)?.CurrentFile;

            var fs = filePaths.ToList();
            new List<ImageContainer> { ImageContainerA, ImageContainerB, ImageContainerC, ImageContainerD, }
                .ForEach(c => c.Load(fs));
        }
    }
}