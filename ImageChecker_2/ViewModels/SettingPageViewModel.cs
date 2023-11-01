using System;
using System.IO;
using ImageChecker_2.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ImageChecker_2.ViewModels
{
    public class SettingPageViewModel : BindableBase, IDialogAware
    {
        private readonly string settingFileName = "setting.xml";

        public event Action<IDialogResult> RequestClose;

        public string Title => "Setting page";

        public Setting Setting { get; set; }
        
        public DelegateCommand CloseCommand => new DelegateCommand(() =>
        {
            RequestClose?.Invoke(new DialogResult());
        });

        public DelegateCommand SetDefaultValueCommand => new DelegateCommand(() =>
        {
            Setting.DrawTagBaseText = "\t<draw a=\"\" b=\"$b\" c=\"$c\" d=\"$d\" />";
            Setting.ImageTagBaseText = "\t<image a=\"$a\" b=\"$b\" c=\"$c\" d=\"$d\" scale=\"$s\" x=\"$x\" y=\"$y\" />";
            Setting.AnimationDrawTagBaseText =
                "\t<anime name=\"draw\" a=\"$a\" b=\"$b\" c=\"$c\" d=\"$d\" scale=\"$s\" x=\"$x\" y=\"$y\" />";

            RaisePropertyChanged(nameof(Setting));
        });
        
        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            Setting.Write(settingFileName, Setting);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Setting = File.Exists(settingFileName) ? Setting.Read(settingFileName) : new Setting();
        }
    }
}