using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{

    abstract class Maska
    {
        public int[,] ElemMaski;
        public byte Rozmiar;
        public int Norma;
        public virtual void Convolve(image_RGB obraz)
        {

            int[,] TempR = new int[obraz.w, obraz.h];
            int[,] TempG = new int[obraz.w, obraz.h];
            int[,] TempB = new int[obraz.w, obraz.h];

            for (int x = 0; x < obraz.w; x++)
            {
                for (int y = 0; y < obraz.h; y++)
                {
                    int SumaR = 0;
                    int SumaB = 0;
                    int SumaG = 0;

                    for (int i = 0; i < Rozmiar; i++)
                    {
                        for (int j = 0; j < Rozmiar; j++)
                        {
                            SumaR += obraz.R[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaB += obraz.G[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaG += obraz.G[x + i - 1][y + j - 1] * ElemMaski[i, j];

                        }
                    }


                    TempR[x, y] = SumaR / Norma;
                    TempG[x, y] = SumaG / Norma;
                    TempB[x, y] = SumaB / Norma;
                }
            }
            for (int c = 0; c < Rozmiar; c++)
            {
                for (int p = 0; p < Rozmiar; p++)
                {
                    obraz.R[c][p] = (byte)TempR[c, p];
                    obraz.G[c][p] = (byte)TempG[c, p];
                    obraz.B[c][p] = (byte)TempB[c, p];
                }


            }
           
        }
    }

    class Sobel 
    {
        public int[,] ElemMaski = new int[,]{
            { -1, -2,-1},
            { 0, 0, 0 },
            { 1, 2, 1 }
            };
        public int[,] ElemMaski1 = new int[,]{
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 } ,
            };
        int Norma=1;
        byte Rozmiar = 3;

        public void Convolve(image_RGB obraz)
        {

            int[,] TempR = new int[obraz.w, obraz.h];
            int[,] TempG = new int[obraz.w, obraz.h];
            int[,] TempB = new int[obraz.w, obraz.h];

            for (int x = 1; x < obraz.w-1; x++)
            {
                for (int y = 1; y < obraz.h-1; y++)
                {
                    int SumaR = 0;
                    int SumaB = 0;
                    int SumaG = 0;

                    int SumaR1 = 0;
                    int SumaB1 = 0;
                    int SumaG1 = 0;

                    for (int i = 0; i < Rozmiar; i++)
                    {
                        for (int j = 0; j < Rozmiar; j++)
                        {
                            SumaR += obraz.R[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaB += obraz.G[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaG += obraz.G[x + i - 1][y + j - 1] * ElemMaski[i, j];
                            SumaR1 += obraz.R[x + i - 1][y + j - 1] * ElemMaski1[i, j];
                            SumaB1 += obraz.G[x + i - 1][y + j - 1] * ElemMaski1[i, j];
                            SumaG1 += obraz.G[x + i - 1][y + j - 1] * ElemMaski1[i, j];

                        }
                    }
                    double[] temp1 = new double[] { SumaR, SumaG, SumaB };
                    double[] temp2 = new double[] { SumaR1, SumaG1, SumaB1 };


                    double tR = (Math.Sqrt(Math.Pow(temp1[0], 2) + Math.Pow(temp2[0], 2)));
                    double tG = (Math.Sqrt((temp1[1]) * (temp1[1]) + (temp2[1]) * (temp2[1])));
                    double tB = (Math.Sqrt((temp1[2]) * (temp1[2]) + (temp2[2]) * (temp2[2])));
                    if (tR > 255)
                        tR = 255;
                    if (tG > 255)
                        tG = 255;
                    if (tB > 255)
                        tB = 255;

                    if (tR < 0)
                        tR = 0;
                    if (tG < 0)
                        tG = 0;
                    if (tB < 0)
                        tB = 0;

                    TempR[x, y] =(byte) tR;
                    TempG[x, y] =(byte) tG;
                    TempB[x, y] =(byte) tB;
                }
            }
            for (int c = 0; c < obraz.w; c++)
            {
                for (int p = 0; p < obraz.h; p++)
                {
                    obraz.R[c][p] = (byte)TempR[c, p];
                    obraz.G[c][p] = (byte)TempG[c, p];
                    obraz.B[c][p] = (byte)TempB[c, p];
                }


            }
        }


    }
}
