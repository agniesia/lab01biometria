using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class image_Gray : image_as_tab
    {
        public byte[][] Greycanal;
        public byte[][] alfa;
        public image_Gray() : base() { }

        public image_Gray(byte[] orginal_tab, int wight, int hight)
            : base(orginal_tab, wight, hight)
        {

            Greycanal = new byte[wight][];
            var k = 0;
            for (int i = 0; i < wight; i++)
            {
                Greycanal[i] = new byte[hight];
                for (int j = 0; j < hight; j++)
                {
                    k = 4 * (j * w + i);
                    Greycanal[i][j] = orginal_tab[k];
                    alfa[i][j] = orginal_tab[k + 3];
                }
            }
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
        public override image_as_tab copy()
        {
            image_Gray temp = new image_Gray();
            temp.Greycanal = (byte[][])this.Greycanal.Clone();
            temp.h = this.h;
            temp.w = this.w;
            temp.alfa = (byte[][])this.alfa.Clone();
            temp.utab = (byte[])this.utab.Clone();
            return temp;
        }
        public override byte[] show()
        {
            return this.utab;
        }
        //public static explicit operator image_Gray(image_RGB rgb)
        //{
        //    // trzeba zrobic coś z akt
        //    image_Gray temp = new image_Gray(rgb.utab, rgb.w, rgb.h);


        //    return temp;
        //}
        






        
    }
}

