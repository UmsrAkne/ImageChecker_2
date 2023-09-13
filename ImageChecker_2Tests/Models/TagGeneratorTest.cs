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

        [Test]
        public void GetTagFromPreviewContainerTest()
        {
            var tag = TagGenerator.GetTag(
                @"<image a=""$a"" $b,$c,$d,$s,$x,$y />",
                new PreviewContainer() 
                {
                    ImageFileA = new ImageFile("aa.png"),
                    ImageFileB = new ImageFile("bb.png"),
                    ImageFileC = new ImageFile("cc.png"),
                    ImageFileD = new ImageFile("dd.png"),
                    Scale = 1.1,
                    X = 100,
                    Y = 200,
                }
            );

            Assert.That(tag, Is.EqualTo(@"<image a=""aa"" bb,cc,dd,1.1,100,200 />"));
        }
    }
}