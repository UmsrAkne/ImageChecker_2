using System;
using System.IO;
using ImageChecker_2.Models;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace ImageChecker_2.ViewModels
{
    public class SettingPageViewModel : IDialogAware
    {
        private readonly string settingFileName = "setting.xml";
        
        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public Setting Setting { get; set; }
        
        public DelegateCommand CloseCommand => new DelegateCommand(() =>
        {
            RequestClose?.Invoke(new DialogResult());
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