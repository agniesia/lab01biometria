using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class Binary:image_Gray
    {
        public int[][] BinaryCanal;
        
        public Binary():base() { }
        public Binary(byte[] orginal_tab, int w, int h):base(orginal_tab,w,h)
            
        {
            BinaryCanal = new int[w][];
            var k = 0;
            for (int i = 0; i < w; i++)
            {
                BinaryCanal[i] = new int[h];
                for (int j = 0; j < h; j++)
                {
                    k = 4 * (j * w + i);
                    BinaryCanal[i][j] = orginal_tab[k];
                }
            }
        }
        public void BinaryAccept(Binaryoperation.BinaryVisitor vistor )
        {
            vistor.VisitBinary(this); 
            
        }
        
    }
}
