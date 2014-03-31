using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class NoiseGeneratorUniformDiffCanal:Visitor
    {
        int chance;
        byte zakres1;
        byte zakres2;
        public NoiseGeneratorUniformDiffCanal(int chance, byte zakres1, byte zakres2)
        {
            //kolorowy szum ale rownokanalowy
            this.chance=chance;
            this.zakres1=zakres1;
            this.zakres2 = zakres2;
        }
        public void rob(image_as_tab image) {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            int[,] tempR = new int[rgb.w, rgb.h];
            int[,] tempG = new int[rgb.w, rgb.h];
            int[,] tempB = new int[rgb.w, rgb.h];
           
            for (int i = 0; i < rgb.w; i++)
            {
               
                for (int j = 0; j < rgb.h; j++) {
                    tempB[i, j] = rgb.B[i][j];
                    tempG[i, j] = rgb.G[i][j];
                    tempR[i, j] = rgb.R[i][j];
                }
            }

            var zakres = rgb.w * rgb.h * chance / 100;
            Random rnd = new Random();
            
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(rgb.w);
                var indexY = rnd.Next(rgb.h);

                var szum1 =(int) (rnd.Next(zakres1, zakres2 + 1) * Math.Pow(-1, rnd.Next(0, 2)));
                var szum2 =(int) (rnd.Next(zakres1, zakres2 + 1) * Math.Pow(-1, rnd.Next(0, 2)));
                var szum3 =(int) (rnd.Next(zakres1, zakres2 + 1) * Math.Pow(-1, rnd.Next(0, 2)));

                //zmienić typ b w sumie potem nie potrzebnie rzutuje a mozna zrobic rzutowanie dopiero przy wyswietlanie
                tempR[indexX,indexY] += szum1;
                tempG[indexX,indexY] += szum2;

                tempB[indexX,indexY] += szum3;
               


            }
            rgb.B = rgb.normalizeimage(tempB);
            rgb.G = rgb.normalizeimage(tempG);
            rgb.R = rgb.normalizeimage(tempR);

        }
        public void Visit(image_Gray Grey)
        {
            //PUSTO
        }
    }
}
