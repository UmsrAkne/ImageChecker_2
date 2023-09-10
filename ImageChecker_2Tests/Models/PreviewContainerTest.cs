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
    }
}