using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class NoiseGeneratorSaltPeper:Visitor
    {
        int chance;
        public NoiseGeneratorSaltPeper(int chance)
        {
            this.chance = chance;

        }
        public void rob(image_as_tab image) {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb){
            var zakres = rgb.w * rgb.h * chance / 100;
            Random rnd = new Random(); 
            
            for (int i=0; i < zakres; i++)
            {
                var indexX = rnd.Next(rgb.w);
                var indexY = rnd.Next(rgb.h);
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
        public void Visit(image_Gray Gray)
        {
            var zakres = Gray.w * Gray.h * chance / 100;
            Random rnd = new Random();
            
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(Gray.w);
                var indexY = rnd.Next(Gray.h);
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
