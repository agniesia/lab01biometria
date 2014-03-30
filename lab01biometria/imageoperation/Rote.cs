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
        public void rob(image_as_tab image) { }
        public void RoateAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_Gray Grey)
        {

            Grey.Greycanal = obrotkanal(Grey.Greycanal, ref Grey.h, ref Grey.h);



        }
        public void Visit(image_RGB rgb)
        {
            rgb.R = obrotkanal(rgb.R, ref rgb.h, ref rgb.w);
            rgb.G = obrotkanal(rgb.G, ref rgb.h, ref rgb.w);
            rgb.B = obrotkanal(rgb.B, ref rgb.h, ref rgb.w);
        }
        
        private byte[][] obrotkanal(byte[][] kanal,ref int w,ref int h ){
            var r = phi * Math.PI / 180;
            var kat = Math.Abs(r);
            int dl = Math.Abs((int)(w * Math.Cos(kat) + h * Math.Sin(kat)));
            int sz = Math.Abs((int)(h * Math.Cos(kat) + w * Math.Sin(kat)));
            var w2 = (w / 2);
            var h2 = (h / 2);
            var imaxnadwa = (dl / 2);
            var jmaxnadwa = (sz / 2);

            byte[][] Newkanal = new byte[dl][];
            for (int i = 0; i < dl; i++)
            {
                Newkanal[i] = new byte[sz];
                for (int j = 0; j < sz; j++)
                {
                    var I = (int)((Math.Cos(-r) * (i - imaxnadwa)) + (Math.Sin(-r) * (j - jmaxnadwa)) + w2);
                    var J = (int)((-Math.Sin(-r) * (i - imaxnadwa)) + (Math.Cos(-r) * (j - jmaxnadwa)) + h2);

                    if ((I >= 0 && J >= 0) && (I < w - 1 && J < h - 1))
                        Newkanal[i][j] = kanal[I][J];
                }
            }
            w = dl;
            h = sz;
            return Newkanal;
        }
    }
    
}
