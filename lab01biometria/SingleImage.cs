using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class SingleImage
    {
        private SingleImage(byte[] source, int w, int h)
        {
            ///new image_as_tab(source,w,h);
        }
        private static  SingleImage Instancja=null;
        public static SingleImage getSingleImage(byte[] source, int w, int h){
            if(Instancja==null){
                Instancja = new SingleImage(source, w, h);
            }
            return Instancja;
        }


    }
}
