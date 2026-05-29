using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class DetectionData
    {
        public string label { get; set; }
        public double confidence { get; set; }
        public bundybox bbox { get; set; }
        public string camera_id { get; set; }
    }
    public class bundybox
    {
        public int x_degeri { get; set; }
        public int y_degeri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
