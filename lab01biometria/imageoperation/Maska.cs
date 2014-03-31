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
        public byte Rozmiar;
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
                            SumaR += rgb.R[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaB += rgb.G[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaG += rgb.G[x + i - 1][y + j - 1] * ElemMaski[i, j];

                        }
                    }


                    TempR[x, y] = SumaR / Norma;
                    TempG[x, y] = SumaG / Norma;
                    TempB[x, y] = SumaB / Norma;
                }
            }
            //powino przypisc null==0 dla brzegów zdjęcia czyli obciac mozna zostawic stare piksele zmianiaj c p mozna tez maske nadkalamc
            for (int c = 0; c < rgb.w; c++)
            {
                for (int p = 0; p < rgb.h; p++)
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
                            SumaGrey += Grey.Greycanal[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            
                        }
                    }


                    TempGrey[x, y] = SumaGrey / Norma;
                    
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
        private int[,] ElemMaski;
        private byte Rozmiar = 3;
        private int Norma = 9;
        public MeanFilterSmooth1(int rozmiar)
        {
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
        public int[,] ElemMaski = { { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } };
        public byte Rozmiar = 3;
        public int Norma = 10;
    }
    class MeanFilterSmooth4 : Maska
    {
        public int[,] ElemMaski = { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
        public byte Rozmiar = 3;
        public int Norma = 16;

    }
    class MeanFiltersharpen5 : Maska
    {
        public int[,] ElemMaski = { { 0, -1, 0}, { -1, 5, -1 }, { 0, -1, 0 } };
        public byte Rozmiar = 3;
        public int Norma = 1;
    }
    class MeanFilterSharpen9 : Maska
    {
        public int[,] ElemMaski = { { 1, -2, 1}, { -2, 9, -2 }, { 1, -2, 1} };
        public byte Rozmiar = 3;
        public int Norma = 1;
    }
    class MeanFilteSharpen5and2 : Maska
    {
        public int[,] ElemMaski = { { 1, -2, 1}, { -2, 5, -2 }, { 1, -2, 1 } };
        public byte Rozmiar = 3;
        public int Norma = 1;
    }
    
}

