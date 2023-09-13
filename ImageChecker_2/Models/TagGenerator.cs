using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ImageChecker_2.Models
{
    public static class TagGenerator
    {
        /// <summary>
        /// baseText と imageFiles の情報を使ってテキストを生成します。
        /// </summary>
        /// <param name="baseText">
        /// この引数に含まれる文字列から　"$a", "$b", "$c", "$d" の４つの文字列が置き換えされます。
        /// </param>
        /// <param name="imageFiles">
        /// 第一引数の baseText の特定の文字列が、 imageFiles の中のファイル名へと置き換えられます。
        /// この引数に入力するリストのサイズは 4 にしてください。サイズがそれ以外の場合は例外がスローされます。
        /// 尚、要素が null の場合は、baseText の該当文字列は空文字に置き換えられます。
        /// </param>
        /// <returns>baseText に置き換え処理を加えた文字列を返します。</returns>
        /// <exception cref="ArgumentException">imageFiles が null であるか、リストのサイズが 4 以外の場合にスローされます。</exception>
        public static string GetTag(string baseText, [NotNull] List<ImageFile> imageFiles)
        {
            if (imageFiles.Count != 4)
            {
                throw new ArgumentException("不正な引数");
            }

            var reps = imageFiles.Select(
                    f => f == null ? string.Empty : Path.GetFileNameWithoutExtension(f.FileInfo.FullName))
                .ToList();

            return baseText
                .Replace("$a", reps[0])
                .Replace("$b", reps[1])
                .Replace("$c", reps[2])
                .Replace("$d", reps[3]);
        }

        public static string GetTag(string baseText, PreviewContainer previewContainer)
        {
            return GetTag(baseText, new List<ImageFile>
                {
                    previewContainer.ImageFileA,
                    previewContainer.ImageFileB,
                    previewContainer.ImageFileC,
                    previewContainer.ImageFileD,
                })
                .Replace("$s", previewContainer.Scale.ToString(CultureInfo.InvariantCulture))
                .Replace("$x", previewContainer.X.ToString(CultureInfo.CurrentCulture))
                .Replace("$y", previewContainer.Y.ToString(CultureInfo.CurrentCulture));
        }
    }
}