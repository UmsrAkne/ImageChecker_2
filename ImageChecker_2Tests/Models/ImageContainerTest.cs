using ImageChecker_2.Models;
using NUnit.Framework;

namespace ImageChecker_2Tests.Models
{
    [TestFixture]
    public class ImageContainerTest
    {
        private string[] FilePaths => new string[]
        {
            "A0101.png",
            "A0102.png",
            "B0101.png",
            "B0102.png",
            "B0103.png",
            "C0101.png",
            "C0102.png",
            "C0103.jpg",
            "testFile.txt",
        };

        [Test]
        public void LoadTest_KeyCharA()
        {
            var imageContainerA = new ImageContainer("A") { Drawing = true, };
            imageContainerA.Load(FilePaths);

            Assert.IsTrue(imageContainerA.Drawing);
            Assert.That(imageContainerA.CurrentFile.FileInfo.Name, Is.EqualTo("A0101.png"));
            Assert.That(imageContainerA.FilteredFiles.Count, Is.EqualTo(2), "抽出された画像ファイルの数は 2 のはず");
            Assert.That(imageContainerA.FilteredFiles[0].FileInfo.Name, Is.EqualTo("A0101.png"));
            Assert.That(imageContainerA.FilteredFiles[1].FileInfo.Name, Is.EqualTo("A0102.png"));
        }

        [Test]
        public void LoadTest_KeyCharB()
        {
            var imageContainerB = new ImageContainer("B") { Drawing = true, };
            imageContainerB.Load(FilePaths);

            Assert.IsTrue(imageContainerB.Drawing);
            Assert.That(imageContainerB.CurrentFile.FileInfo.Name, Is.EqualTo("B0101.png"));
            Assert.That(imageContainerB.FilteredFiles.Count, Is.EqualTo(3), "抽出された画像ファイルの数は 3 のはず");
            Assert.That(imageContainerB.FilteredFiles[0].FileInfo.Name, Is.EqualTo("B0101.png"));
            Assert.That(imageContainerB.FilteredFiles[1].FileInfo.Name, Is.EqualTo("B0102.png"));
            Assert.That(imageContainerB.FilteredFiles[2].FileInfo.Name, Is.EqualTo("B0103.png"));
        }

        [Test]
        public void LoadTest_KeyCharC()
        {
            var imageContainerC = new ImageContainer("C") { Drawing = true, };
            imageContainerC.Load(FilePaths);

            Assert.IsTrue(imageContainerC.Drawing);
            Assert.That(imageContainerC.CurrentFile.FileInfo.Name, Is.EqualTo("C0101.png"));
            Assert.That(imageContainerC.FilteredFiles.Count, Is.EqualTo(3), "抽出された画像ファイルの数は 3 のはず");
            Assert.That(imageContainerC.FilteredFiles[0].FileInfo.Name, Is.EqualTo("C0101.png"));
            Assert.That(imageContainerC.FilteredFiles[1].FileInfo.Name, Is.EqualTo("C0102.png"));
            Assert.That(imageContainerC.FilteredFiles[2].FileInfo.Name, Is.EqualTo("C0103.jpg"));
        }
    }
}