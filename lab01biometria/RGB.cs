using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class image_RGB : image_as_tab
    {
        public byte[][] B;
        public byte[][] G;
        public byte[][] R;
        public byte[][] alfa;
        public byte[] RCanal;
        public byte[] GCanal;
        public byte[] BCanal;

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }

        public image_RGB(byte[] sourcePixels, int wight, int hight)
            : base(sourcePixels, wight, hight)
        {


            byte[] RCanal = utab.Where((x, i) => i % 4 == 0).ToArray();
            byte[] BCanal = utab.Where((x, i) => i % 4 == 1).ToArray();
            byte[] GCanal = utab.Where((x, i) => i % 4 == 2).ToArray();

            int k = 0;
            w = wight;
            h = hight;

            B = new byte[wight][];
            G = new byte[wight][];
            R = new byte[wight][];
            alfa = new byte[wight][];
            for (int i = 0; i < wight; i++)
            {
                B[i] = new byte[hight];
                G[i] = new byte[hight];
                R[i] = new byte[hight];
                alfa[i] = new byte[hight];
                for (int j = 0; j < hight; j++)
                {

                    k = 4 * (j * w + i);
                    B[i][j] = sourcePixels[k];
                    G[i][j] = sourcePixels[k + 1];
                    R[i][j] = sourcePixels[k + 2];
                    alfa[i][j] = sourcePixels[k + 3];

                }
            }
        }

        public override byte[] show()
        {
            return imagearray3Dto1D();
        }
        



        public byte[] imagearray3Dto1D()
        {

            byte[] temp = new byte[w * h * 4];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {

                    temp[4 * (j * w + i)] = B[i][j];
                    temp[4 * (j * w + i) + 1] = G[i][j];
                    temp[4 * (j * w + i) + 2] = R[i][j];
                    temp[4 * (j * w + i) + 3] = alfa[i][j];


                }
            }
            return temp;
        }

        //public static explicit operator image_Gray(image_RGB rgb)
        //{
        //    // trzeba zrobic coś z akt
        //    image_Gray temp = new image_Gray(rgb.utab,rgb.w,rgb.h);
            

        //    return temp;
        //}




        
        //public image_Gray greyimage()
        //{
        //    byte z;
        //    byte[] temp = new byte[w * h * 4];
        //    for (int i = 0; i < w; i++)
        //    {
        //        for (int j = 0; j < h; j++)
        //        {
        //            z = (byte)((this.B[i][j] + this.G[i][j] + this.R[i][j]) / 3);
        //            temp[4 * (j * w + i)] = z;
        //            temp[4 * (j * w + i) + 1] = z;
        //            temp[4 * (j * w + i) + 2] = z;
        //            temp[4 * (j * w + i) + 3] = alfa[i][j];
        //        }

        //    }
        //    return new image_Gray(temp, w, h);

        //}

        //public image_Gray grey_naturalimage()
        //{
        //    byte z;
        //    byte[] temp = new byte[w * h * 4];
        //    for (int i = 0; i < w; i++)
        //    {
        //        for (int j = 0; j < h; j++)
        //        {
        //            //0.3*R+0.59*G+0.11*B
        //            z = (byte)(0.11 * this.B[i][j] + 0.59 * this.G[i][j] + 0.3 * this.R[i][j]);
        //            temp[4 * (j * w + i)] = z;
        //            temp[4 * (j * w + i) + 1] = z;
        //            temp[4 * (j * w + i) + 2] = z;
        //            temp[4 * (j * w + i) + 3] = alfa[i][j];

        //        }

        //    }
        //    return new image_Gray(temp, w, h);

        //}

 

   
   
        
        
        
    


    }
}
