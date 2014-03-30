using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Negative:Visitor
    {
        public void rob(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            rgb.utab = (rgb.utab.Select((x, i) => (i + 1) % 4 == 0 ? x : (byte)(255 - x)).ToArray());
            int k = 0;
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {

                    k = 4 * (j * rgb.w + i);
                    rgb.B[i][j] = rgb.utab[k];
                    rgb.G[i][j] = rgb.utab[k + 1];
                    rgb.R[i][j] = rgb.utab[k + 2];
                    

                }
            }
        }
        public void Visit(image_Gray Grey)
        {
            Grey.utab = (Grey.utab.Select((x, i) => (i + 1) % 4 == 0 ? x : (byte)(255 - x)).ToArray());
            // uwaga zmien wyswietlanie albo cos niespojnosc w przechowywaniu danych
            var k = 0;
            for (int i = 0; i < Grey.w; i++)
            {
                for (int j = 0; j < Grey.h; j++)
                {

                    k = 4 * (j * Grey.w + i);
                    Grey.Greycanal[i][j] = Grey.utab[k];

       

                }
            }
        }
    }
}
