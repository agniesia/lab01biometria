using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class BinaryLocalMean:Visitor
    {
        int local;
        public BinaryLocalMean(int local)
        {
            this.local = local;
        }
        public void rob(image_as_tab image) {
            BinaryLocalMeanAll(image);
        }
        public void BinaryLocalMeanAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            // do poprawy
            image_Gray Grey = new image_Gray();
            RGBtoGrey v = new RGBtoGrey();
            v.RGBtoGreyAll(rgb);

            Grey = v.GreyElement;
            Visit(Grey);
            
        }
        public void Visit(image_Gray Grey)
        {
            byte zero = 0, one = 255;
            byte[][] Temp = new byte[Grey.w][];
            int range = (int)local / 2;
            for (int x = range; x < Grey.w - range; x++)
            {
                Temp[x] = new byte[Grey.h];
                for (int y = range; y < Grey.h - range; y++)
                {
                    var Suma = 0;
                    for (int i = 0; i < local; i++)
                    {
                        for (int j = 0; j < local; j++)
                        {
                            Suma += Grey.Greycanal[x + i -range ][y + j - range];

                        }

                    }
                    var localmean = Suma / Math.Pow(local, 2);
                    Temp[x][y] = Grey.Greycanal[x][y] >= localmean ? one : zero;
                }
            }
            for (int x = range; x < Grey.w - range; x++)
            {

                for (int y = range; y < Grey.h - range; y++)
                    Grey.Greycanal[x][y] = (byte)Temp[x][y];
            }
        }
    }
}
