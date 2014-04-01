using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class GaussFilter:Visitor
    {
        
        double Sigma;
        public double[,] ElemMaski;
        public int Rozmiar;
        public double Norma;
        
        
        public GaussFilter(int rozmiar, double sigma)
        {
            Rozmiar = rozmiar;
            Sigma = sigma;
            ElemMaski = new double[Rozmiar, Rozmiar];
            
            for (int i = 0; i < rozmiar; i++)
            {
                for (int j = 0; j < rozmiar; j++)
                {

                    ElemMaski[i, j] =(1/(2*Math.PI*Sigma*Sigma)*Math.Exp(-(i*i+j*j)/(2*Sigma*Sigma)));
                    Norma += ElemMaski[i, j];
                }
            }
            


        }
        public void  GaussFilterAll(image_as_tab image){
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
           GaussFilterAll(image);

        }

        public void Visit(image_RGB rgb)
        {
            //convolve rgb
            double[,] TempR = new double[rgb.w, rgb.h];
            double[,] TempG = new double[rgb.w, rgb.h];
            double[,] TempB = new double[rgb.w, rgb.h];

            byte start = (byte)(Rozmiar / 2);
            for (int x = start; x < rgb.w - start; x++)
            {
                for (int y = start; y <rgb.h - start; y++)
                {
                    double SumaR = 0;
                    double SumaB = 0;
                    double SumaG = 0;

                    for (int i = 0; i < Rozmiar; i++)
                    {
                        for (int j = 0; j < Rozmiar; j++)
                        {
                            SumaR += rgb.R[x + i - start][y + j - start] * ElemMaski[i, j];
                            SumaB += rgb.B[x + i - start][y + j - start] * ElemMaski[i, j];
                            SumaG += rgb.G[x + i - start][y + j - start] * ElemMaski[i, j];

                        }
                    }


                    TempR[x, y] = SumaR / Norma;
                    TempG[x, y] = SumaG / Norma;
                    TempB[x, y] = SumaB / Norma;
                   if ( TempR[x, y] > 255)
                         TempR[x, y] = 255;
                    if (TempG[x, y] > 255)
                        TempG[x, y]= 255;
                    if (TempB[x, y] > 255)
                        TempB[x, y] = 255;

                    if ( TempR[x, y] < 0)
                         TempR[x, y]= 0;
                    if (TempG[x, y]< 0)
                        TempG[x, y] = 0;
                    if (TempB[x, y]< 0)
                        TempB[x, y] = 0;

                    
                
                }
            }
            //powino przypisc null==0 dla brzegów zdjęcia czyli obciac mozna zostawic stare piksele zmianiaj c p mozna tez maske nadkalamc
            for (int c = start; c < rgb.w-start; c++)
            {
                for (int p = start; p < rgb.h-start; p++)
                {
                    rgb.R[c][p] = (byte)TempR[c, p];
                    rgb.G[c][p] = (byte)TempG[c, p];
                    rgb.B[c][p] = (byte)TempB[c, p];
                }


            }
           
        }
        public void Visit(image_Gray Grey) {

            double[,] TempGrey = new double[Grey.w, Grey.h];


            byte start = (byte)(Rozmiar/ 2);
            for (int x = start; x < Grey.w - start; x++)
            {
                for (int y = start; y < Grey.h - start; y++)
                {
                    double SumaGrey = 0;
                   

                    for (int i = 0; i < Rozmiar; i++)
                    {
                        for (int j = 0; j < Rozmiar; j++)
                        {
                            SumaGrey += (Grey.Greycanal[x + i - start][y + j - start] * ElemMaski[i, j]);
                            
                        }
                    }


                    SumaGrey = SumaGrey / Norma;
                    if (SumaGrey > 255)
                    {
                        SumaGrey = 255;
                    }
                    else if(SumaGrey<0)
                    {
                        SumaGrey=0;
                    }
                    TempGrey[x, y] = SumaGrey;
                    
                }
            }
            
            for (int c = 0; c < Grey.w; c++)
            {
                for (int p = 0; p < Grey.h; p++)
                {
                    
                    Grey.Greycanal[c][p] = (byte)TempGrey[c, p];
                    
                }


            }
           
        }
        
        
    }
        

    
}
