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
           
        }

        private void ale(Binary binary){
            List<Tuple<int,int>> bufor=new List<Tuple<int,int>>();
            List<int> wspolrzedne=new List<int>();
        
            int wsp_minx= binary.w;
            int wsp_miny=binary.h;
            int wsp_maxx=0;
            int wsp_maxy=0;
            for (int y = 0; y < binary.h; y++)
            {
                for (int x = 0; x < binary.w; x++)
                {
                    if(binary.BinaryCanal[x][y]==1){
                        bufor.Add(new Tuple<int,int>(x,y));

                    }
                }
            }
            
          
              
              bufor.append([it.multi_index[0],it.multi_index[1]])
              self.obraz[it.multi_index[0],it.multi_index[1]]=[20,100,50]
              
              
          it.iternext()
          while len(bufor)!=0:
                  k=0
                  for b in bufor:
                    
                    self.obraz[b[0],b[1]]=[20,100,50]
                    if (wsp_minx>b[0]):
                        wsp_minx=b[0]
                    if wsp_miny>b[1]:
                        wsp_miny=b[1]
                    if (wsp_maxx<b[0]):
                        wsp_maxx=b[0] 
                    if (wsp_maxy<b[1]):
                        wsp_maxy=b[1]
                    if (self.obraz[b[0]+1,b[1]-1,0])==1:
                        
                        self.obraz[b[0]+1,b[1]-1]=[20,100,50]
                        bufor.append([b[0]+1,b[1]-1])
                    if (self.obraz[b[0]-1,b[1]+1,0])==1:
                        
                        bufor.append([b[0]-1,b[1]+1])
                        self.obraz[b[0]-1,b[1]+1]=[20,100,50]
                       
                    if (self.obraz[b[0]-1,b[1],0])==1:
                        
                        self.obraz[b[0]-1,b[1]]=[20,100,50]
                        bufor.append([b[0]-1,b[1]])
                    if (self.obraz[b[0]-1,b[1]-1,0])==1:
                       
                        self.obraz[b[0]-1,b[1]-1]=[20,100,50]
                        bufor.append([b[0]-1,b[1]-1])
                    if (self.obraz[b[0],b[1]+1,0])==1:
                        
                        self.obraz[b[0],b[1]+1]=[20,100,50]
                        bufor.append([b[0],b[1]+1])
                    if (self.obraz[b[0],b[1]-1,0])==1:
                       
                        self.obraz[b[0],b[1]-1]=[20,100,50]
                        bufor.append([b[0],b[1]-1])
                    if (self.obraz[b[0]+1,b[1],0])==1:
                       
                        self.obraz[b[0]+1,b[1]]=[20,100,50]
                        bufor.append([b[0]+1,b[1]])
                    if (self.obraz[b[0]+1,b[1]+1,0])==1:
                        
                        self.obraz[b[0]+1,b[1]+1]=[20,100,50]
                        bufor.append([b[0]+1,b[1]+1])
                   
                    del bufor[k]
                    k=k+1
                  if len(bufor)==0:
                      wspolrzedne.append([[wsp_minx,wsp_miny],[wsp_maxx,wsp_maxy]])
                      wsp_minx=10000
                      wsp_miny=10000
                      wsp_maxx=0
                      wsp_maxy=0     
          
    }

        
        
    }
}
