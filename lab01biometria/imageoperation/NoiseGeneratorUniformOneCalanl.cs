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
        public void rob(image_as_tab image) { }
        public void  Visit(image_RGB rgb){
            var zakres = rgb.w * rgb.h * chance / 100;
            Random rnd = new Random();
            byte[] IndexRandom = new byte[zakres];
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(rgb.R.GetLength(0));
                var indexY = rnd.Next(rgb.R.GetLength(1));

                byte szum =(byte)rnd.Next(zakres1,zakres2);
                if (rnd.Next(0, 2) == 0)
                {
                    rgb.R[indexX][indexY] -= szum;
                    rgb.G[indexX][indexY] -= szum;
                    rgb.B[indexX][indexY] -= szum;
                }
                else
                {
                    rgb.R[indexX][indexY] += szum;
                    rgb.G[indexX][indexY] += szum;
                    rgb.B[indexX][indexY] += szum;

                }

            }
            //NORMALIZCJA (napisz funkcje)
        
        }
        public void Visit(image_Gray Grey)
        {
            //szary szum 
            var zakres = Grey.w * Grey.h * chance / 100;
            Random rnd = new Random();
            byte[] IndexRandom = new byte[zakres];
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(Grey.Greycanal.GetLength(0));
                var indexY = rnd.Next(Grey.Greycanal.GetLength(1));

                byte szum = (byte)rnd.Next(zakres1, zakres2);
                if (rnd.Next(0, 2) == 0)
                {
                    Grey.Greycanal[indexX][indexY] -= szum;
                    
                }
                else
                {
                   Grey.Greycanal[indexX][indexY] += szum;

                }

            }
            //NORMALIZCJA (napisz funkcje)
        
        }

    }
}
