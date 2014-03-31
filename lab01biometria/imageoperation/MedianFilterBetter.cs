using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class MedianFilterBetter : Visitor
    {
        int local;
        public MedianFilterBetter(int local)
        {
            this.local = local;
        }
        public void MedianFilterBetterAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            MedianFilterBetterAll(image);

        }
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
                    int cruvepixels =(int) Math.Round((decimal)(local * local * 0.2));
                    var firstindex=mediana.IndexOf(Grey.Greycanal[x][y]);
                    var lastindex =mediana.LastIndexOf(Grey.Greycanal[x][y]);
                    if (firstindex < cruvepixels || lastindex > ((local * local) - cruvepixels))
                        TempGrey[x, y] = medialvalue;
                    else
                        TempGrey[x, y] = Grey.Greycanal[x][y];
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
             List<byte> medianaR = new List<byte>(100);
             List<byte> medianaG = new List<byte>(100);
             List<byte> medianaB = new List<byte>(100);
             byte start = (byte)(local / 2);
             
             for (int x = start ; x < rgb.w - start; x++)
             {
                 for (int y = start; y < rgb.h - start; y++)
                 {



                     for (int i = 0; i < local; i++)
                     {
                         for (int j = 0; j < local; j++)
                         {
                             medianaR.Add(rgb.R[x + i - start][y + j - start]);
                             medianaG.Add(rgb.G[x + i - start][y + j - start]);
                             medianaB.Add(rgb.B[x + i - start][y + j - start]);

                         }
                     }
                     medianaR.Sort();
                     medianaG.Sort();
                     medianaB.Sort();
                     int pierwszy = (int)((local * local) / 2);//zadziala o ile indeksowanie jest od zera

                     byte medialvalueR = (byte)(medianaR.ElementAt(pierwszy));
                     byte medialvalueG = (byte)(medianaG.ElementAt(pierwszy));
                     byte medialvalueB = (byte)(medianaB.ElementAt(pierwszy));
                     int cruvepixels = (int)Math.Round((decimal)(local * local * 0.2));
                     var firstindexR = medianaR.IndexOf(rgb.R[x][y]);
                     var lastindexR = medianaR.LastIndexOf(rgb.R[x][y]);
                     var firstindexG = medianaG.IndexOf(rgb.G[x][y]);
                     var lastindexG = medianaG.LastIndexOf(rgb.G[x][y]);
                     var firstindexB = medianaB.IndexOf(rgb.B[x][y]);
                     var lastindexB = medianaB.LastIndexOf(rgb.B[x][y]);
                     if (firstindexR < cruvepixels || lastindexR > ((local * local) - cruvepixels))
                         TempR[x, y] = medialvalueR;
                     else
                         TempR[x, y] = rgb.R[x][y];

                     if (firstindexG < cruvepixels || lastindexG > ((local * local) - cruvepixels))
                         TempG[x, y] = medialvalueG;
                     else
                         TempG[x, y] = rgb.G[x][y];

                     if (firstindexB < cruvepixels || lastindexB > ((local * local) - cruvepixels))
                         TempB[x, y] = medialvalueB;
                     else
                         TempB[x, y] = rgb.B[x][y];

                     
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
