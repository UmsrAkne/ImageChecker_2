using System.Collections.Generic;
using System.Drawing;

namespace ImageChecker_2.Models
{
    public class History
    {
        public string TagText { get; set; }

        public double Scale { get; set; }

        public Point Pos { get; set; }

        public Point DisplayPos { get; set; }
        
        /// <summary>
        /// ImageFiles のリストです。要素数の最大値は 4 です。
        /// デフォルトでは全ての要素に null が入っています。
        /// </summary>
        /// <value>
        /// ImageFiles のリストです。
        /// </value>
        public List<ImageFile> ImageFiles { get; private set; } = new (4) { null, null, null, null, };
    }
}