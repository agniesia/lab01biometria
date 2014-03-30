using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Otsu:Visitor
    {
        public void OtsuAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) { }
        public void Visit(image_RGB rgb) {

            //zmiana na grey jak beda funckje
            image_Gray Grey = new image_Gray();
            RGBtoGrey v = new RGBtoGrey();
            v.RGBtoGreyAll(rgb);

            Grey = v.GreyElement;
            Visit(Grey);
           
            
            //copis na 2 D binary
        }
        public void Visit(image_Gray Grey) { 
            int[] Hist = new int[256];
            for (int k = 0; k < 256; k++)
            {
                Hist[k] = Grey.utab.Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
            }
            var suma = 0;
            byte one = 1;
            byte zero = 0;
            var total = Grey.w * Grey.h;
            for (int k = 0; k < 256; k++)
            {
                suma += Hist[k] * k;
            }
            var sumB = 0;
            var wB = 0;
            var wF = 0;
            var mB=0;
            var mF=0;
            var max = 0.0;
            var between = 0.0;
            var threshold1 = 0.0;
            var threshold2 = 0.0;
            for (var i = 0; i < 256; ++i) {
                wB += Hist[i];
                if (wB == 0)
                    continue;
                wF = total - wB;
                if (wF == 0)
                    break;
                sumB += i * Hist[i];
                mB = sumB / wB;
                mF = (suma - sumB) / wF;
                between = wB * wF * Math.Pow(mB - mF, 2);
                if ( between >= max ) {
                    threshold1 = i;
                    if ( between > max ) {
                        threshold2 = i;
                    }
                    max = between;            
                }
            }
            var progowanie= ( threshold1 + threshold2 ) / 2.0;
            Grey.utab = Grey.utab.Select(x => x >= progowanie ? one : zero).ToArray();
            //copis na 2 D binary
        
        
        }

    }
}
