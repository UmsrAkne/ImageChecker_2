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
        public void LoadTest()
        {
            var imageContainerA = new ImageContainer("A") { Drawing = true, };
            imageContainerA.Load(FilePaths);

            Assert.IsTrue(imageContainerA.Drawing);
            Assert.That(imageContainerA.CurrentFile.FileInfo.Name, Is.EqualTo("A0101.png"));
            Assert.That(imageContainerA.FilteredFiles[0].FileInfo.Name, Is.EqualTo("A0101.png"));
            Assert.That(imageContainerA.FilteredFiles[1].FileInfo.Name, Is.EqualTo("A0102.png"));
        }
    }
}