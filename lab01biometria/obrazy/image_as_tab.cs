using lab01biometria;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System.Threading;

namespace lab01biometria

{
    
    abstract class image_as_tab
    {
        public byte[] utab;
        public int w;
        public int h;
        
        public image_as_tab() { }
        public image_as_tab(byte[] orginal_tab, int wight, int hight)
        {
            w = wight;
            h = hight;
            utab = new byte[orginal_tab.Length];
        utab =(byte[]) orginal_tab.Clone();
        }
        public abstract void Accept(Visitor visitor);

        public abstract byte[] show();
        public abstract image_as_tab copy();

}
 

}
