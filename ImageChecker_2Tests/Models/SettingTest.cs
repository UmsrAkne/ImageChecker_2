using System.IO;
using ImageChecker_2.Models;
using NUnit.Framework;

namespace ImageChecker_2Tests.Models
{
    [TestFixture]
    public class SettingTest
    {
        [TearDown]
        public void Cleanup()
        {
            File.Delete("test.setting");
        }
        
        [Test]
        public void WriteTest()
        {
            var setting = new Setting();
            Setting.Write("test.setting", setting);
            Assert.That(File.Exists("test.setting"), Is.True);
            
            // 2回連続で実行しても大丈夫か確認する。
            Setting.Write("test.setting", setting);
        }
        
        [Test]
        public void ReadTest()
        {
            var setting = new Setting();
            Assert.That(setting.ImageTagBaseText, Is.EqualTo(string.Empty), "初期状態では空文字なのを確認する");
            setting.ImageTagBaseText = "imageTag";
            Setting.Write("test.setting", setting);
            
            Assert.That(Setting.Read("test.setting").ImageTagBaseText, Is.EqualTo("imageTag"));
        }
    }
}