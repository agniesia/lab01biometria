using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Bernsen:Visitor
    {
        int local;
        public Bernsen(int local)
        {
            this.local = local;
        }
        public void BernsenAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb) {
            //szry obrazek
            RGBtoGrey v = new RGBtoGrey();
            v.RGBtoGreyAll(rgb);

            image_Gray Grey = v.GreyElement;
            Visit(Grey);
        }
        public void Visit(image_Gray Grey) {
            byte zero = 0, one = 1;
            var globalmean = Grey.utab.Sum(x => x) / Grey.utab.Length;
            byte epsilon = 1;
            var Suma = 0;
            byte[][] Temp = new byte[Grey.w][];
            for (int x = 1; x < Grey.w - 1; x++)
            {
                Temp[x] = new byte[Grey.h];
                for (int y = 1; y < Grey.h - 1; y++)
                {
                    Suma = 0;
                    byte[] temp = new byte[local * local];
                    for (int i = 0; i < local; i++)
                    {

                        for (int j = 0; j < local; j++)
                        {
                            temp[i] = Grey.Greycanal[x + i - 1][y + j - 1];

                        }

                    }

                    var TempMax = temp.Max();
                    var TempMin = temp.Min();
                    var level = (TempMax + TempMin) / 2;


                    if ((TempMax - TempMax) < epsilon)
                    {
                        Temp[x][y] = Grey.Greycanal[x][y] >= globalmean ? one : zero;
                    }
                    else
                        //mozna dodac +/- sigma ale konieczny dodatkowy warunek(mysle ze lepsze z sigma)
                        Temp[x][y] = Grey.Greycanal[x][y] >= level ? one : zero;
                }
            }
            //Grey.Greycanal = (byte[][])Temp.Clone();

        }
    }
}
