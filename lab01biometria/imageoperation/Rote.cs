using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Roate : Visitor
    {
        int phi;
        public Roate(int phi)
        {
            this.phi = phi;

        }
        public void rob(image_as_tab image) {
            RoateAll(image);

        }
        public void RoateAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_Gray Grey)
        {

            Grey.Greycanal = obrotkanal(Grey.Greycanal, Grey.h,  Grey.w);
            Grey.alfa = (byte[][])obrotkanal(Grey.alfa, Grey.h, Grey.w).Clone();
            var r = phi * Math.PI / 180;

            var kat = Math.Abs(r);
            int dl = Math.Abs((int)(Grey.w * Math.Cos(kat) + Grey.h * Math.Sin(kat)));
            int sz = Math.Abs((int)(Grey.h * Math.Cos(kat) + Grey.w * Math.Sin(kat)));
            Grey.h = sz;
            Grey.w = dl;


        }
        public void Visit(image_RGB rgb)
        {
            rgb.R =(byte[][]) obrotkanal(rgb.R,  rgb.h, rgb.w).Clone();
            rgb.G = (byte[][])obrotkanal(rgb.G, rgb.h, rgb.w).Clone();
            rgb.B = (byte[][])obrotkanal(rgb.B, rgb.h, rgb.w).Clone();
            rgb.alfa = (byte[][])obrotkanal(rgb.alfa, rgb.h, rgb.w).Clone();
            var r = phi * Math.PI / 180;
           
            var kat = Math.Abs(r);
            int dl = Math.Abs((int)(rgb.w * Math.Cos(kat) + rgb.h * Math.Sin(kat)));
            int sz = Math.Abs((int)(rgb.h * Math.Cos(kat) + rgb.w * Math.Sin(kat)));
            rgb.h = sz;
            rgb.w = dl;
        }
        
        private byte[][] obrotkanal(byte[][] kanal,int hi,int wi ){
            var r = phi * Math.PI / 180;
            int h = hi;
            int w = wi;
            var kat = Math.Abs(r);
            int dl = Math.Abs((int)(w * Math.Cos(kat)   + h * Math.Sin(kat)));
            int sz = Math.Abs((int)(h * Math.Cos(kat)  + w * Math.Sin(kat)));
            var w2 = (w / 2);
            var h2 = (h / 2);
            var imaxnadwa = (dl / 2);
            var jmaxnadwa = (sz / 2);

            byte[][] Newkanal = new byte[dl][];
            for (int i = 0; i < dl; i++)
            {
                Newkanal[i] = new byte[sz];
            }
            for (int i = 0; i < dl; i++)
            {
                
                for (int j = 0; j < sz; j++)
                {
                    var I = (int)((Math.Cos(-r) * (i - imaxnadwa)) + (Math.Sin(-r) * (j - jmaxnadwa)) + w2);
                    var J = (int)((-Math.Sin(-r) * (i - imaxnadwa)) + (Math.Cos(-r) * (j - jmaxnadwa)) + h2);

                    if ((I >= 0 && J >= 0))
                        if (I < (w-1) && J < (h-1))
                            Newkanal[i][j] = kanal[I][J];
                }
            }
           
            return Newkanal;
        }
    }
    
}
