using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class MedianFilter:Visitor
    {
        int local;
        public MedianFilter(int local)
        {
            this.local = local;
        }
        public void MedianFilterAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) { }
        public void Visit(image_Gray Grey){

            int[,] TempGrey = new int[Grey.w, Grey.h];
            List<byte> mediana=new List<byte>();
            byte start=(byte)(local/2);
            for (int x = start; x < Grey.w - start; x++)
            {
                for (int y = start; y < Grey.h - start; y++)
                {



                    for (int i = 0; i < local; i++)
                    {
                        for (int j = 0; j < local; j++)
                        {
                            mediana.Add(Grey.Greycanal[x + i - 1][y + j - 1]);

                        }
                    }
                    mediana.Sort();
                    int pierwszy = (int)((local * local) / 2);

                    byte medialvalue = (byte)(mediana.ElementAt(pierwszy));
                    TempGrey[x, y] = medialvalue;
                    mediana.Clear();
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
         public void Visit(image_RGB rgb){
             int[,] TempR = new int[rgb.w, rgb.h];
             int[,] TempG = new int[rgb.w, rgb.h];
             int[,] TempB = new int[rgb.w, rgb.h];
             List<byte> medianaR = new List<byte>();
             List<byte> medianaG = new List<byte>();
             List<byte> medianaB = new List<byte>();
             byte start = (byte)(local / 2);
             for (int x = start; x < rgb.w - start; x++)
             {
                 for (int y = start; y < rgb.h - start; y++)
                 {



                     for (int i = 0; i < local; i++)
                     {
                         for (int j = 0; j < local; j++)
                         {
                             medianaR.Add(rgb.R[x + i - 1][y + j - 1]);
                             medianaG.Add(rgb.G[x + i - 1][y + j - 1]);
                             medianaB.Add(rgb.B[x + i - 1][y + j - 1]);

                         }
                     }
                     medianaR.Sort();
                     medianaG.Sort();
                     medianaB.Sort();
                     int pierwszy = (int)((local * local) / 2);

                     byte medialvalueR = (byte)(medianaR.ElementAt(pierwszy));
                     byte medialvalueG = (byte)(medianaG.ElementAt(pierwszy));
                     byte medialvalueB = (byte)(medianaB.ElementAt(pierwszy));
                     TempG[x, y] = medialvalueG;
                     TempR[x, y] = medialvalueR;
                     TempB[x, y] = medialvalueB;
                     medianaR.Clear();
                     medianaG.Clear();
                     medianaB.Clear();
                 }
             }
             for (int c = 0; c < rgb.w; c++)
             {
                 for (int p = 0; p <rgb.h; p++)
                 {
                     rgb.G[c][p]=  (byte)TempG[c, p];
                     rgb.R[c][p] = (byte)TempR[c, p];
                     rgb.B[c][p] = (byte)TempB[c, p]; 
                     

                 }
             }
        }
    }
}
