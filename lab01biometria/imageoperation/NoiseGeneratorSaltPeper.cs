using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class NoiseGeneratorSaltPeper
    {
        int chance;
        public NoiseGeneratorSaltPeper(int chance)
        {
            this.chance = chance;

        }
        public void Visitor(image_RGB rgb){
            var zakres = rgb.w * rgb.h * chance / 100;
            Random rnd = new Random(); 
            byte[] IndexRandom=new byte[zakres];
            for (int i=0; i < zakres; i++)
            {
                var indexX = rnd.Next(rgb.R.GetLength(0));
                var indexY = rnd.Next(rgb.R.GetLength(1));
                if (rnd.Next(0, 2)==0){
                    rgb.R[indexX][indexY] = 255;
                    rgb.G[indexX][indexY] = 255;
                    rgb.B[indexX][indexY] = 255;
                }
                else
                {
                    rgb.R[indexX][indexY] = 0;
                    rgb.G[indexX][indexY] = 0;
                    rgb.B[indexX][indexY] = 0;

                }
            }

        }
        public void Visitor(image_Gray Gray)
        {
            var zakres = Gray.w * Gray.h * chance / 100;
            Random rnd = new Random();
            byte[] IndexRandom = new byte[zakres];
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(Gray.Greycanal.GetLength(0));
                var indexY = rnd.Next(Gray.Greycanal.GetLength(1));
                if (rnd.Next(0, 2) == 0)
                {
                    Gray.Greycanal[indexX][indexY] = 255;
                    
                }
                else
                {
                    Gray.Greycanal[indexX][indexY] = 0;
                    

                }
            }


        }
    }
}
