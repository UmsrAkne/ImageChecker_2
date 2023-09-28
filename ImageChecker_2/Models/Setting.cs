using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ImageChecker_2.Models
{
    public class Setting
    {
        public string ImageTagBaseText { get; set; } = string.Empty;

        public string DrawTagBaseText { get; set; } = string.Empty;

        public string AnimationDrawTagBaseText { get; set; } = string.Empty;

        public static Setting Read(string targetFileName)
        {
            var serializer2 = new XmlSerializer(typeof(Setting));
            using (var sr = new StreamReader(targetFileName, new UTF8Encoding(false)))
            {
                return (Setting)serializer2.Deserialize(sr);
            }
        }

        public static void Write(string destFileName, Setting setting)
        {
            var serializer1 = new XmlSerializer(typeof(Setting));
            using (var sw = new StreamWriter(destFileName, false, new UTF8Encoding(false)))
            {
                serializer1.Serialize(sw, setting);
            }
        }
    }
}