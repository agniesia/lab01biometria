using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class NoiseGeneratorUniformOneCalanl:Visitor
    {
        int chance;
        byte zakres1;
        byte zakres2;
        public NoiseGeneratorUniformOneCalanl(int chance, byte zakres1, byte zakres2)
        {
            //kolorowy szum ale rownokanalowy
            this.chance=chance;
            this.zakres1=zakres1;
            this.zakres2 = zakres2;
        }
        public void NoiseGeneratorUniformOneCalanlAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            image.Accept(this);
        }
        public void  Visit(image_RGB rgb){
            var zakres = rgb.w * rgb.h * chance / 100;
            Random rnd = new Random();
            int[,] tempR = new int[rgb.w, rgb.h];
            int[,] tempG = new int[rgb.w, rgb.h];
            int[,] tempB = new int[rgb.w, rgb.h];
            for (int i = 0; i < rgb.w; i++)
            {

                for (int j = 0; j < rgb.h; j++)
                {
                    tempB[i, j] = rgb.B[i][j];
                    tempG[i, j] = rgb.G[i][j];
                    tempR[i, j] = rgb.R[i][j];
                }
            }

            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(rgb.w);
                var indexY = rnd.Next(rgb.h);

                byte szum =(byte)rnd.Next(zakres1,zakres2);
                if (rnd.Next(0, 2) == 0)
                {
                    
                    tempR[indexX,indexY] -= szum;
                    tempG[indexX,indexY] -= szum;
                    tempB[indexX,indexY] -= szum;
                }
                else
                {
                    tempR[indexX,indexY] += szum;
                    tempG[indexX,indexY] += szum;
                    tempB[indexX,indexY] += szum;

                }

            }
            rgb.R = rgb.normalizeimage(tempR);
            rgb.G = rgb.normalizeimage(tempG);
            rgb.B = rgb.normalizeimage(tempB);
            //NORMALIZCJA (napisz funkcje)
        
        }
        public void Visit(image_Gray Grey)
        {
            //szary szum 
            var zakres = Grey.w * Grey.h * chance / 100;
            Random rnd = new Random();
            int[,] temp = new int[Grey.w, Grey.h];
            for (int i = 0; i < Grey.w; i++)
            {

                for (int j = 0; j < Grey.h; j++)
                {
                    temp[i, j] = Grey.Greycanal[i][j];
                    
                }
            }

            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(Grey.w);
                var indexY = rnd.Next(Grey.h);

                byte szum = (byte)rnd.Next(zakres1, zakres2);
                if (rnd.Next(0, 2) == 0)
                {
                    temp[indexX,indexY] -= szum;
                    
                }
                else
                {
                   temp[indexX,indexY] += szum;

                }
                Grey.Greycanal = Grey.normalizeimage(temp);

            }
            //NORMALIZCJA (napisz funkcje)
        
        }

    }
}
