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
        public void Visit(image_RGB rgb)
        {
            var zakres = rgb.w * rgb.h * chance / 100;
            Random rnd = new Random();
            byte[] IndexRandom = new byte[zakres];
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(rgb.R.GetLength(0));
                var indexY = rnd.Next(rgb.R.GetLength(1));

                var szum1 = rnd.Next(zakres1, zakres2 + 1) * Math.Pow(-1, rnd.Next(0, 2));
                var szum2 = rnd.Next(zakres1, zakres2 + 1) * Math.Pow(-1, rnd.Next(0, 2));
                var szum3 = rnd.Next(zakres1, zakres2 + 1) * Math.Pow(-1, rnd.Next(0, 2));

                //zmienić typ b w sumie potem nie potrzebnie rzutuje a mozna zrobic rzutowanie dopiero przy wyswietlanie
                //R[indexX][indexY] += szum1;
                //G[indexX][indexY] += szum2;
                //B[indexX][indexY] += szum3;
            }

        }
        public void Visit(image_Gray Grey)
        {
            //PUSTO
        }
    }
}
