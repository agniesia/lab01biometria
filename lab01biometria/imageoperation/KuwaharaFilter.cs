﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class KuwaharaFilter : Visitor
    {
        int Rozmiar;

        public KuwaharaFilter(int Rozmiar = 7)
        {
            this.Rozmiar = Rozmiar;
        }
        public void rob(image_as_tab image)
        {

            KuwaharaFilterAll(image);
        }
        public void KuwaharaFilterAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            int[,] TempR = new int[rgb.w, rgb.h];
            int[,] TempG = new int[rgb.w, rgb.h];
            int[,] TempB = new int[rgb.w, rgb.h];
            TempR = Kuwaharacanal(rgb.R, rgb.w, rgb.h);
            TempG = Kuwaharacanal(rgb.G, rgb.w, rgb.h);
            TempB = Kuwaharacanal(rgb.B, rgb.w, rgb.h);
            var start = (int)Rozmiar / 2;
            for (int c = start; c < rgb.w - start; c++)
            {
                for (int p = start; p < rgb.h - start; p++)
                {
                    rgb.R[c][p] = (byte)TempR[c, p];
                    rgb.G[c][p] = (byte)TempG[c, p];
                    rgb.B[c][p] = (byte)TempB[c, p];

                }
            }


        }
        public void Visit(image_Gray Grey)
        {
            int[,] TempGrey = new int[Grey.w, Grey.h];
            TempGrey=Kuwaharacanal(Grey.Greycanal, Grey.w, Grey.h);
            var start = (int)Rozmiar / 2;
            for (int c = start; c < Grey.w-start; c++)
            {
                for (int p = start; p < Grey.h-start; p++)
                {
                    Grey.Greycanal[c][p] = (byte)TempGrey[c, p];

                }
            }


        }
        private int[,] Kuwaharacanal(byte [][] canal, int w, int h ){
            int[,] TempGrey = new int[w, h];
            int Sr1;
            int Sr2;
            int Sr3;
            int Sr4;
            List<int> maska1= new List<int>();
            List<int> maska2= new List<int>();
            List<int> maska3= new List<int>();
            List<int> maska4= new List<int>();



            byte start = (byte)(Rozmiar/ 2);
            for (int x = start; x < w - start; x++)
            {
                for (int y = start; y < h - start; y++)
                {
                    Sr1=0;
                    Sr2=0;
                    Sr3=0;
                    Sr4=0;

                    for (int i = 0; i < start; i++)
                    {
                        for (int j = 0; j < start; j++)
                        {
                            maska1.Add( canal[x + i][y + j]);
                            maska2.Add(canal[x -i][y +j]);
                            maska3.Add( canal[x -i][y - j]);
                            maska4.Add (canal[x + i][y - j]);

                            
                        }
                    }
                    var minimask=start*start;
                    Sr1=maska1.Sum()/minimask;
                    Sr2=maska2.Sum()/ minimask;
                    Sr3=maska3.Sum()/minimask;
                    Sr4=maska4.Sum()/minimask;
                    double variancja1=0;
                    double variancja2=0;
                    double variancja3=0;
                    double variancja4=0;
                    for(int i=0 ;i<minimask;i++){
                        variancja1+=Math.Pow((maska1[i]-Sr1),2);
                        variancja2+=Math.Pow((maska2[i]-Sr2),2);
                        variancja3+=Math.Pow((maska3[i]-Sr3),2);
                        variancja4+=Math.Pow((maska4[i]-Sr4),2);
                    }
                    variancja1=variancja1/minimask;
                    variancja2=variancja2/minimask;
                    variancja3=variancja3/minimask;
                    variancja4=variancja4/minimask;
                    maska1.Clear();
                    maska2.Clear();
                    maska3.Clear();
                    maska4.Clear();
                    List<double> variation=new List<double>{variancja1,variancja2,variancja3,variancja4};
                    List<double> Sr=new List<double>{Sr1,Sr2,Sr3,Sr4};
                    
                    TempGrey[x,y]=(int)Sr[variation.IndexOf(variation.Min())];
                    variation.Clear();
                    Sr.Clear();

                }
            }
            return TempGrey;
        }
            
            


       

    }   
}
