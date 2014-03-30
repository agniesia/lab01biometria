using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.Binaryoperation
{
    interface BinaryVisitor
    {
        void VisitBinary(Binary binary);
    }
    class Segmentation:BinaryVisitor
    {
        public void SegemtationAll(Binary binary )
        {
            binary.BinaryAccept(this);

        }
        public void VisitBinary(Binary binary)
        {
            segment(binary);
        }

        private void segment(Binary binary)
        {
            List<Tuple<int, int>> bufor = new List<Tuple<int, int>>();
            List<Tuple<int, int, int, int>> wspolrzedne = new List<Tuple<int, int, int, int>>();

            int wsp_minx = binary.w;
            int wsp_miny = binary.h;
            int wsp_maxx = 0;
            int wsp_maxy = 0;
            byte kolor = 2;
            for (int y = 0; y < binary.h; y++)
            {
                for (int x = 0; x < binary.w; x++)
                {
                    if (binary.BinaryCanal[x][y] == 1)
                    {
                        bufor.Add(new Tuple<int, int>(x, y));
                        binary.BinaryCanal[x][y] = kolor;
                        while (bufor.Count != 0)
                        {


                            foreach (Tuple<int, int> element in bufor.ToList())
                            {
                                binary.BinaryCanal[element.Item1][element.Item2] = kolor;
                                wsp_minx = element.Item1 < wsp_minx ? element.Item1 : wsp_minx;
                                wsp_maxx = element.Item1 > wsp_maxx ? element.Item1 : wsp_maxx;

                                wsp_miny = element.Item1 < wsp_miny ? element.Item1 : wsp_miny;
                                wsp_maxy = element.Item1 > wsp_maxy ? element.Item1 : wsp_maxy;
                                int p;
                                int d;
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        p = element.Item1 + i - 1;
                                        d = element.Item2 + j - 1;
                                        if (binary.BinaryCanal[p][d] != 1)
                                            continue;
                                        binary.BinaryCanal[p][d] = kolor;
                                        bufor.Add(new Tuple<int, int>(p, d));
                                    }
                                }

                                bufor.RemoveAt(0);
                            }


                            if (bufor.Count == 0)
                            {

                                wspolrzedne.Add(new Tuple<int, int, int, int>(wsp_minx, wsp_miny, wsp_maxx, wsp_maxy));
                                wsp_minx = binary.w;
                                wsp_miny = binary.h;
                                wsp_maxx = 0;
                                wsp_maxy = 0;
                                kolor++;
                            }
                        }
                    }
                }
            }
        }

            
        
            
          
              
            
             
              
              
          
         
   
        
    }
}
