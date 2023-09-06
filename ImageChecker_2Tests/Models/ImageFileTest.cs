using ImageChecker_2.Models;
using NUnit.Framework;

namespace ImageChecker_2Tests.Models
{
    public class ImageFileTest
    {
        [Test]
        public void IsMatchingNamingRuleTest()
        {
            // [A-D] までのアルファベットに数字四桁　ならマッチする。
            Assert.IsTrue(new ImageFile("A0000").IsMatchingNamingRule);
            Assert.IsTrue(new ImageFile("B0101").IsMatchingNamingRule);
            Assert.IsTrue(new ImageFile("C0201").IsMatchingNamingRule);
            Assert.IsTrue(new ImageFile("D9999").IsMatchingNamingRule);
            Assert.IsTrue(new ImageFile("A0101L").IsMatchingNamingRule, "余計な文字がくっついているのはルール上問題ない");

            Assert.IsFalse(new ImageFile("E0101").IsMatchingNamingRule);
            Assert.IsFalse(new ImageFile("A000").IsMatchingNamingRule);
            Assert.IsFalse(new ImageFile("A").IsMatchingNamingRule);
            Assert.IsFalse(new ImageFile("test").IsMatchingNamingRule);
        }

        [Test]
        public void IndexTest()
        {
            Assert.That(new ImageFile("A0102").Index, Is.EqualTo(1));
            Assert.That(new ImageFile("A0102").SubIndex, Is.EqualTo(2));

            Assert.That(new ImageFile("A1122").Index, Is.EqualTo(11), "二桁までは可");
            Assert.That(new ImageFile("A1122").SubIndex, Is.EqualTo(22), "二桁までは可");

            Assert.That(new ImageFile("A01022").Index, Is.EqualTo(1), "最後の余分な数字は認識されない。");
            Assert.That(new ImageFile("A01022").SubIndex, Is.EqualTo(2), "最後の余分な数字は認識されない。");
        }
    }
}