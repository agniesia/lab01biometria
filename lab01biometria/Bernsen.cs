﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Bernsen:Visitor
    {
        int local;
        int sigma;
        public Bernsen(int local, int sigma)
        {
            this.local = local;
            this.sigma = sigma;
        }
        public void BernsenAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            BernsenAll(image);

        }
        public void Visit(image_RGB rgb) {
            //szry obrazek
            RGBtoGrey v = new RGBtoGrey();
            v.RGBtoGreyAll(rgb);

            image_Gray Grey = v.GreyElement;
            Visit(Grey);
        }
        public void Visit(image_Gray Grey) {
            byte zero = 0, one = 255;
            var globalmean = Grey.utab.Sum(x => x) / Grey.utab.Length;
          
            var Suma = 0;
            int range = (int)local / 2;
            byte[][] Temp = new byte[Grey.w][];
            for (int x = range; x < Grey.w - range; x++)
            {
                Temp[x] = new byte[Grey.h];
                for (int y = range; y < Grey.h - range; y++)
                {
                    Suma = 0;
                    byte[] temp = new byte[local * local];
                    for (int i = 0; i < local; i++)
                    {

                        for (int j = 0; j < local; j++)
                        {
                            temp[i] = Grey.Greycanal[x + i - range][y + j - range];

                        }

                    }

                    var TempMax = temp.Max();
                    var TempMin = temp.Min();
                    var level = (TempMax + TempMin) / 2;


                    if ((TempMax - TempMax) <sigma )
                    {
                        Temp[x][y] = Grey.Greycanal[x][y] >= globalmean ? one : zero;
                    }
                    else
                        //mozna dodac +/- sigma ale konieczny dodatkowy warunek(mysle ze lepsze z sigma)
                        Temp[x][y] = Grey.Greycanal[x][y] >= level ? one : zero;
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
