using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{

    abstract class Maska:Visitor
    {
        public int[,] ElemMaski;
        public int Rozmiar;
        public int Norma;

        public void MaskaAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            MaskaAll(image);

        }

        public void Visit(image_RGB rgb)
        {
            //convolve rgb
            int[,] TempR = new int[rgb.w, rgb.h];
            int[,] TempG = new int[rgb.w, rgb.h];
            int[,] TempB = new int[rgb.w, rgb.h];

            byte start = (byte)(Rozmiar / 2);
            for (int x = start; x < rgb.w - start; x++)
            {
                for (int y = start; y <rgb.h - start; y++)
                {
                    int SumaR = 0;
                    int SumaB = 0;
                    int SumaG = 0;

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

            int[,] TempGrey = new int[Grey.w, Grey.h];


            byte start = (byte)(Rozmiar/ 2);
            for (int x = start; x < Grey.w - start; x++)
            {
                for (int y = start; y < Grey.h - start; y++)
                {
                    int SumaGrey = 0;
                   

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

    

    class MeanFilterSmooth1 : Maska
    {
        
        public MeanFilterSmooth1(int rozmiar)
        {
            Rozmiar = rozmiar;
            Norma = rozmiar*rozmiar;
            ElemMaski=new int[rozmiar,rozmiar];
            for (int i = 0; i < rozmiar; i++)
            {
                for (int j = 0; j < rozmiar; j++)
                {

                    ElemMaski[i, j] = 1;
                }
            }
        }
    }
    class MeanFilterSmooth2 : Maska
    {
        
        public MeanFilterSmooth2()
        {
            ElemMaski = new [,]{ { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } };
            Rozmiar = 3;
            Norma = 10;

        }
    }
    class MeanFilterSmooth4 : Maska
    {
       
        public MeanFilterSmooth4()
        {
            ElemMaski = new[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
            Rozmiar = 3;
            Norma = 16;

        }

    }
    class MeanFiltersharpen5 : Maska
    {
        public MeanFiltersharpen5()
        {
            ElemMaski = new int[,]{ { 0, -1, 0}, { -1, 5, -1 }, { 0, -1, 0 } };
            Rozmiar = 3;
            Norma = 1;
        }
    }
    class MeanFilterSharpen9 : Maska
    {
        
        public MeanFilterSharpen9(){
            ElemMaski =new int[,] { { -1, -1, -1}, { -1, 9, -1 }, { -1, -1, -1} };
            Rozmiar = 3;
            Norma = 1;
        }
    }
    class MeanFilteSharpen5and2 : Maska
    {
        
        public MeanFilteSharpen5and2()
        {
            ElemMaski =new int[,] { { 1, -2, 1}, { -2, 5, -2 }, { 1, -2, 1 } };
            Rozmiar = 3;
            Norma = 1;
        }
    }
    
}

