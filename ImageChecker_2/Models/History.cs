using System.Drawing;

namespace ImageChecker_2.Models
{
    public class History
    {
        public string TagText { get; set; }

        public double Scale { get; set; }

        public Point Pos { get; set; }

        public Point DisplayPos { get; set; }
        
        public ImageFile ImageFileA { get; set; }
        
        public ImageFile ImageFileB { get; set; }
        
        public ImageFile ImageFileC { get; set; }
        
        public ImageFile ImageFileD { get; set; }
    }
}