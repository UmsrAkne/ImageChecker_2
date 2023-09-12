using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageChecker_2.Models
{
    public static class TagGenerator
    {
        public static string GetTag(string baseText, List<ImageFile> imageFiles)
        {
            if (imageFiles.Count < 3 || imageFiles.Any(i => i.FileInfo == null))
            {
                throw new ArgumentException("不正な引数");
            }
            
            var a = Path.GetFileNameWithoutExtension(imageFiles[0].FileInfo.FullName);
            var b = Path.GetFileNameWithoutExtension(imageFiles[1].FileInfo.FullName);
            var c = Path.GetFileNameWithoutExtension(imageFiles[2].FileInfo.FullName);
            var d = Path.GetFileNameWithoutExtension(imageFiles[3].FileInfo.FullName);

            return baseText
                .Replace("$a", a)
                .Replace("$b", b)
                .Replace("$c", c)
                .Replace("$d", d);
        }
    }
}