using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class GaussFilter:Maska
    {
        
        double Sigma;
        
        public double[,] ElemMaski;
        public int Rozmiar;
        public int Norma;
        public void rob(image_as_tab image) { }
        public GaussFilter(int rozmiar, double sigma)
        {
            this.Rozmiar = rozmiar;
            this.Sigma = sigma;
            ElemMaski = new double[Rozmiar, Rozmiar];
            Norma = 1;
            for (int i = 0; i < rozmiar; i++)
            {
                for (int j = 0; j < rozmiar; j++)
                {

                    ElemMaski[i, j] = 1/(2*Math.PI*Sigma*Sigma)*Math.Exp(-(i*i+j*j)/(2*Sigma*Sigma));
                    
                }
            }


        }
        public void  GaussFilterAll(image_as_tab image){
            image.Accept(this);
        }
        

    }
}
