using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab01biometria.imageoperation
{
    class Scalebilinear:Visitor
    {
        double scale;
        public Scalebilinear(double scale)
        {
            if (scale<1)
            {
                throw new BadImputFunkcionExeption();
            }
            this.scale=scale;

        }
        public void rob(image_as_tab image) {
            ScalebilinearAll(image);
        }
        public void ScalebilinearAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_Gray Grey)
        {
            if (scale>2){

            Grey.Greycanal = kanalinterpolation(Grey.Greycanal, Grey.w, Grey.h);
            Grey.alfa = kanalinterpolation(Grey.Greycanal, Grey.w, Grey.h);
            Grey.h =(int) ((Grey.h)*scale);
            Grey.w = (int)((Grey.w )*scale);
            }
        }
        public void Visit(image_RGB rgb)
        {
            if (scale > 2)
            {
                rgb.R = kanalinterpolation(rgb.R, rgb.w, rgb.h);
                rgb.G = kanalinterpolation(rgb.G, rgb.w, rgb.h);
                rgb.B = kanalinterpolation(rgb.B, rgb.w, rgb.h);
                rgb.alfa = kanalinterpolation(rgb.alfa, rgb.w, rgb.h);
                rgb.h=(int)((rgb.h+1) * scale);
                rgb.w = (int)((rgb.w + 1) * scale);
            }
        }
        private byte[][] kanalinterpolation(byte[][] kanal,int w, int h){
            int newh = (int)((h+1) * scale);
            int neww =(int) ((w+1) * scale);
            byte[][] Newkanal = new byte[neww][];
            int tx=0;
            int ty=0;
            for (int x = 0; x < neww; x++)
            {
                Newkanal[x] = new byte[newh];
            }

            for (int x = 0; x < w; x++)
            {
               
            
                for (int y = 0; y < h; y++)
                {
                    tx=(int)(x*scale);
                    ty=(int)(y*scale);
                    Newkanal[tx][ty] = kanal[x][y];
                }
            }
            //for (int x = 0; x < w-1; x++)
            //{
            //    for (int y = 0; y < h - 1; y++)
            //    {
            //        tx = (int)(x * scale);
            //        ty =(int)( y * scale);


            //        var q00 = Newkanal[tx][ty +(int) scale];
            //        var q01 = Newkanal[tx + (int)scale][ty + (int)scale];
            //        var q11 = Newkanal[tx + (int)scale][ty];

            //        for (int i = tx + 1; i < tx + scale; i++)
            //        {
            //            for (int j = ty + 1; j < ty + scale; j++)
            //            {

            //                var temp = q00 * (1 - i) * (j - y) + q01 * i * (1 - y) + Newkanal[tx][ty] + q11 * i * j;
            //                if (temp > 255)
            //                    temp = 255;
            //                else if (temp < 0)
            //                    temp = 0;
            //                Newkanal[i][j] = (byte)temp;


            //            }
            //        }
            //    }
            //}
                    

            return Newkanal;
        }

    }
}
