using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class BinaryLocalGlobal:Visitor
    {
        int local;
        int sigma;
        public BinaryLocalGlobal(int local,int sigma)
        {
            this.local = local;
            this.sigma = sigma;
        }
        public void rob(image_as_tab image) { }
        public void BinaryLocalGlobalAll(image_as_tab image)
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

        public void Visit(image_Gray Grey)
        {
            byte zero = 0, one = 1;
            var globalmean = Grey.utab.Sum(x => x) / Grey.utab.Length;
            byte[][] Temp = new byte[Grey.w][];
            for (int x = 1; x < Grey.w - 1; x++)
            {
                Temp[x] = new byte[Grey.h];
                for (int y = 1; y < Grey.h - 1; y++)
                {
                    var Suma = 0;
                    for (int i = 0; i < local; i++)
                    {
                        for (int j = 0; j < local; j++)
                        {
                            Suma += Grey.Greycanal[x + i - 1][y + j - 1];

                        }

                    }
                    var localmean = Suma / Math.Pow(local, 2);

                    if ((globalmean - sigma < localmean) && (localmean < globalmean + sigma))
                    {
                        Temp[x][y] = Grey.Greycanal[x][y] >= localmean ? one : zero;
                    }
                    else
                        //mozna dodac +/- sigma ale konieczny dodatkowy warunek(mysle ze lepsze z sigma)
                        Temp[x][y] = Grey.Greycanal[x][y] >= globalmean? one : zero;
                }
            }
            Grey.Greycanal = (byte[][])Temp.Clone();//binryzacja bez brzegów 
        }
        
        
    }
}
