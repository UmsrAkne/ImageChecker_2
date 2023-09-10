using ImageChecker_2.Models;
using NUnit.Framework;

namespace ImageChecker_2Tests.Models
{
    [TestFixture]
    public class PreviewContainerTest
    {
        [Test]
        public void ScaleTest()
        {
            var previewContainer = new PreviewContainer { Scale = 1.0, };
            Assert.That(previewContainer.ActualScale, Is.EqualTo(0.25));

            previewContainer.Scale = 2.0;
            Assert.That(previewContainer.ActualScale, Is.EqualTo(0.5));
        }

        [Test]
        public void XTest()
        {
            var previewContainer = new PreviewContainer { Scale = 1.0, X = 640, };
            Assert.That(previewContainer.ActualX, Is.EqualTo(160));

            previewContainer.X = 320;
            Assert.That(previewContainer.ActualX, Is.EqualTo(80));
        }
        
        [Test]
        public void YTest()
        {
            var previewContainer = new PreviewContainer { Scale = 1.0, Y = 160, };
            Assert.That(previewContainer.ActualY, Is.EqualTo(40));

            previewContainer.Y = 40;
            Assert.That(previewContainer.ActualY, Is.EqualTo(10));
        }
    }
}