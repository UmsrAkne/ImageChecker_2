using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace ImageChecker_2.Models
{
    public class ImageFile : BindableBase
    {
        private int width;
        private int height;
        private bool isSelected;

        public ImageFile(string filePath)
        {
            FileInfo = new FileInfo(filePath);
            if (File.Exists(filePath))
            {
                var bitmap = new Bitmap(filePath);
                Width = bitmap.Width;
                Height = bitmap.Height;
            }

            if (!Regex.Match(FileInfo.Name, @"[ABCD]\d\d\d\d").Success)
            {
                return;
            }

            IsMatchingNamingRule = true;
            MatchCollection matches = Regex.Matches(FileInfo.Name, @"[ABCD](\d\d)(\d\d)");
            Index = int.Parse(matches[0].Groups[1].Value);
            SubIndex = int.Parse(matches[0].Groups[2].Value);
        }

        public int Width { get => width; set => SetProperty(ref width, value); }

        public int Height { get => height; set => SetProperty(ref height, value); }

        public bool IsMatchingNamingRule { get; private set; }

        public int Index { get; private set; }

        public FileInfo FileInfo { get; }

        public int SubIndex { get; set; }

        public bool IsSelected { get => isSelected; set => SetProperty(ref isSelected, value); }

        public override string ToString()
        {
            return FileInfo.Name;
        }
    }
}