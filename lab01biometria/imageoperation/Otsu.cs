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
        public void rob(image_as_tab image) {
            OtsuAll(image);
        }
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
                Hist[k] = Grey.Greycanal.AsParallel().SelectMany(t=>t).ToArray().AsParallel().Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
            }
            var maxi = Hist.Max();
            //Hist = Hist.AsParallel().Select(x => x = x / maxi).ToArray();
            var suma = 0;
            byte one = 255;
            byte zero = 0;
            var total = Grey.w * Grey.h;
            for (int k = 1; k < 256; k++)
            {
                suma += Hist[k] * k;
            }

            double sumB = 0;
            double wB = 0;
            double wF = 0;
            double mB = 0;
            double mF = 0;
            double max = 0.0;
            double between = 0.0;
            double threshold1 = 0.0;
            double threshold2 = 0.0;
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
            for (int x = 0; x < Grey.w ; x++)
            {

                for (int y = 0; y < Grey.h; y++)
                    Grey.Greycanal[x][y] = Grey.Greycanal[x][y] <= progowanie ? one : zero;
            }
            //copis na 2 D binary
        
        
        }

    }
}
