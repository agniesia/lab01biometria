using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class ThresholdingGlobal:Visitor
    {
        public void ThresholdingGlobalAll(image_as_tab image, int local)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            image_Gray Grey = new image_Gray();
            RGBtoGrey v = new RGBtoGrey();
            v.RGBtoGreyAll(rgb);

            Grey = v.GreyElement;
            Visit(Grey);
            
        }
        public void rob(image_as_tab image) { }
        public void Visit(image_Gray Grey)
        {
            var mean = Grey.utab.Sum(x => x) / Grey.utab.Length;
            byte one = 1;
            byte zero = 0;
            Grey.utab = Grey.utab.Select(x => x >= mean ? one : zero).ToArray();

        }
    }
}
