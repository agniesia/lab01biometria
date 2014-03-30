using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class KuwaharaFilter:Visitor
    {
        int Rozmiar;
        
        public KuwaharaFilter(int Rozmiar=5){
            this.Rozmiar=Rozmiar;
        }
        public void rob(image_as_tab image) { }
        public void KuwaharaFilterAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb) { }
        public void Visit(image_Gray Grey)
        {
            int[,] TempGrey = new int[Grey.w, Grey.h];
            int Sr1;
            int Sr2;
            int Sr3;
            int Sr4;
            List<int> maska1= new List<int>();
            List<int> maska2= new List<int>();
            List<int> maska3= new List<int>();
            List<int> maska4= new List<int>();



            byte start = (byte)(Rozmiar/ 2);
            for (int x = start; x < Grey.w - start; x++)
            {
                for (int y = start; y < Grey.h - start; y++)
                {
                    Sr1=0;
                    Sr2=0;
                    Sr3=0;
                    Sr4=0;

                    for (int i = 0; i < start; i++)
                    {
                        for (int j = 0; j < start; j++)
                        {
                            maska1.Add( Grey.Greycanal[x + i][y + j]);
                            maska2.Add(Grey.Greycanal[x -i][y +j]);
                            maska3.Add( Grey.Greycanal[x -i][y - j]);
                            maska4.Add (Grey.Greycanal[x + i][y - j]);

                            
                        }
                    }
                    var minimask=start*start;
                    Sr1=maska1.Sum()/minimask;
                    Sr2=maska2.Sum()/ minimask;
                    Sr3/=maska3.Sum()/minimask;
                    Sr4/=maska4.Sum()/minimask;
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
                    List<double> var=new List<double>{variancja1,variancja2,variancja3,variancja4};
                    List<double> Sr=new List<double>{Sr1,Sr2,Sr3,Sr4};
                    
                    TempGrey[x,y]=(int)Sr[var.IndexOf(var.Min())];
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
