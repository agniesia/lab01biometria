using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class Maska
    {
        public int[][] Sobel1 = new int[][]{
            new int[] { -1, -2, -1 },
            new int[] { 0, 0, 0 },
            new int[] { 1, 2, 1 }
            };
        public int[][] Sobel2 = new int[][]{
            new int[] { -1, 0, 1 },
            new int[] { -2, 0, 2 },
            new int[] { -1, 0, 1 } ,
            };
        
 

    }
}
