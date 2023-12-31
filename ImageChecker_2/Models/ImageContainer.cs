using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Prism.Mvvm;

namespace ImageChecker_2.Models
{
    public class ImageContainer : BindableBase
    {
        private readonly string keyChar;
        private List<ImageFile> filteredFiles = new List<ImageFile>();
        private bool drawing;
        private int selectedIndex;
        private ImageFile currentFile;
        private bool isEnabled = true;
        
        public ImageContainer(string keyChar)
        {
            this.keyChar = keyChar;
        }
        
        public event EventHandler CurrentFileChanged;

        public List<ImageFile> FilteredFiles
        {
            get => filteredFiles;
            private set => SetProperty(ref filteredFiles, value);
        }

        public ImageFile CurrentFile
        {
            get => IsEnabled ? currentFile : null;
            set
            {
                if (currentFile != null)
                {
                    currentFile.IsSelected = false;
                }

                if (value != null)
                {
                    value.IsSelected = true;
                }

                if (SetProperty(ref currentFile, value))
                {
                    CurrentFileChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool Drawing { get => drawing; set => SetProperty(ref drawing, value); }

        public int SelectedIndex { get => selectedIndex; set => SetProperty(ref selectedIndex, value); }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (SetProperty(ref isEnabled, value))
                {
                    CurrentFileChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private List<ImageFile> Files { get; set; } = new List<ImageFile>();

        public void Load(IEnumerable<string> filePaths)
        {
            Files = filePaths
                .Where(path => path.EndsWith(".png") || path.EndsWith(".jpg"))
                .Where(path => Path.GetFileName(path).Contains(keyChar))
                .Select(path => new ImageFile(path))
                .ToList();

            FilteredFiles = Files.ToList();
            CurrentFile = Files.FirstOrDefault();
            IsEnabled = Files.Count > 0;
            
            if (Files.Count == 0)
            {
                Drawing = false;
            }
        }

        public void SelectSameGroupImages(ImageFile baseImageFile)
        {
            if (keyChar == "A")
            {
                return;
            }

            var groupIndex = baseImageFile.Index;

            FilteredFiles = Files.Where(imageFile => imageFile.Index == groupIndex).ToList();
            CurrentFile = FilteredFiles.FirstOrDefault();
        }

        public string GetCurrentFileName()
        {
            if (!Drawing || CurrentFile == null)
            {
                return string.Empty;
            }

            return Path.GetFileNameWithoutExtension(CurrentFile.FileInfo.FullName);
        }

        public void SetImageByName(string name)
        {
            Drawing = !string.IsNullOrWhiteSpace(name);
            if (Drawing)
            {
                CurrentFile =
                    FilteredFiles.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f.FileInfo.Name) == name);
            }
        }
    }
}