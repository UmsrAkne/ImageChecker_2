using System;
using System.Collections.Generic;
using ImageChecker_2.Models;
using NUnit.Framework;

namespace ImageChecker_2Tests.Models
{
    [TestFixture]
    public class TagGeneratorTest
    {
        [Test]
        public void GetTagTest()
        {
            var tag = TagGenerator.GetTag(
                @"<image a=""$a"" $b,$c,$d />",
                new List<ImageFile>()
                {
                    new ImageFile("aa.png"),
                    new ImageFile("bb.png"),
                    new ImageFile("cc.png"),
                    new ImageFile("dd.png"),
                }
            );

            Assert.That(tag, Is.EqualTo(@"<image a=""aa"" bb,cc,dd />"));
        }
        
        [Test]
        public void GetTagNullTest()
        {
            var tag = TagGenerator.GetTag(
                @"<image a=""$a"" $b,$c,$d />",
                new List<ImageFile>()
                {
                    new ImageFile("aa.png"),
                    null,
                    null,
                    null,
                }
            );

            Assert.That(tag, Is.EqualTo(@"<image a=""aa"" ,, />"));
        }

        [Test]
        public void GetTagErrorTest()
        {
            // ImageFile のリストのカウントが 4 以外の場合は例外がスローされる。
            // ArgumentException が発生するかテストする。
            Assert.Throws<ArgumentException>(() =>
                TagGenerator.GetTag(
                    @"<image a=""$a"" $b,$c,$d />",
                    new List<ImageFile>()
                    {
                        new ImageFile("aa.png"),
                        new ImageFile("bb.png"),
                        new ImageFile("cc.png"),
                    }
                )
            );
        }
    }
}