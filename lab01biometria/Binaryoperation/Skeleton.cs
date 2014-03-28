using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.Binaryoperation
{
    class Skeleton:BinaryVisitor
    {
        private static int[][] A = new int[][] {
        new int[]{3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135,143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254},
        new int[]{7, 14, 28, 56, 112, 131, 193, 224},
        new int[]{7, 14, 15, 28, 30, 56, 60, 112, 120, 131, 135, 193, 195, 224, 225, 240},
        new int[]{7, 14, 15, 28, 30, 31, 56, 60, 62, 112, 120, 124, 131, 135, 143, 193, 195, 199, 224, 225, 227, 240, 241, 248},
        new int[]{7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 193, 195, 199, 207, 224, 225, 227, 231, 240, 241, 243, 248, 249, 252},
        new int[]{7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 191, 193, 195, 199, 207, 224, 225, 227, 231, 239, 240, 241, 243, 248, 249, 251, 252, 254}
         };

        private static int[] A1pix = { 3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254 };
        private static int[][] maska = { new int[] { 128, 64, 32 }, new int[] { 1, 0, 16 }, new int[] { 2, 4, 8 } };
        public void SkeletonAll(Binary binary)
        {
            binary.BinaryAccept(this);
        }
        public void  VisitBinary(Binary binary){
            Skeletonize(binary);
        }
        private void countwaga(Binary binary)
        {
            for (int y = 0; y < binary.h; y++)
            {
                for (int x = 0; x < binary.w; x++)
                {
                    var Suma = 0;
                    if (binary.BinaryCanal[x][y] > 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {

                                Suma += binary.BinaryCanal[x + i - 1][y + j - 1] >= 1 ? maska[i][j] : 0;
                            }
                        }
                        binary.BinaryCanal[x][y] = Suma;
                    }
                }
            }
        }
        private void wagaforbinary(Binary binary)
        {
            for (int y = 0; y < binary.h; y++)
            {
                for (int x = 0; x < binary.w; x++)
                {
                    
                    if (binary.BinaryCanal[x][y] == 1)
                    {
                        var Suma = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {

                                Suma += binary.BinaryCanal[x + i - 1][y + j - 1] == 1 ? maska[i][j] : 0;
                            }
                        }
                        binary.BinaryCanal[x][y] = Suma;
                    }
                }
            }
        }
            
            private void delete(Binary binary,int[] maska){
                for (int y = 0; y < binary.h; y++)
                {
                    for (int x = 0; x < binary.w; x++)
                    {
                        if (maska.Contains(binary.BinaryCanal[x][y]))
                        {
                            binary.BinaryCanal[x][y] = 0;
                            flage = true;
                        }
                    }
                }
            }
            private void border(Binary binary, int[] maska)
            {
                for (int y = 0; y < binary.h; y++)
                {
                    for (int x = 0; x < binary.w; x++)
                    {
                        if (maska.Contains(binary.BinaryCanal[x][y]))
                            binary.BinaryCanal[x][y] = 2;
                    }
                }
            }
            private void changetoone(Binary binary)
            {
                for (int y = 0; y < binary.h; y++)
                {
                    for (int x = 0; x < binary.w; x++)
                    {
                        if (binary.BinaryCanal[x][y]>0)
                            binary.BinaryCanal[x][y] = 1;
                    }
                }
            }
            static bool flage;
            private void Skeletonize(Binary binary)
            {
                
                flage=true;
                while (flage == true)
                {
                    wagaforbinary(binary);
                    border(binary, A[0]);
                    flage = false;
                    foreach (int[] elmaska in A)
                    {
                        countwaga(binary);
                        delete(binary, elmaska);

                    }
                    
                    changetoone(binary);
                    
                }
                wagaforbinary(binary);
                delete(binary, A1pix);


            }

        
    }
}


