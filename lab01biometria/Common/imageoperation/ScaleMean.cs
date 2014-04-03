using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class ScaleMean : Visitor
    {
        double skala;
        public ScaleMean(double skala)
        {
            if (skala <= 0 )
                throw new BadImputFunkcionExeption();
            this.skala = skala;
        }
        public void rob(image_as_tab image) {
            ScaleMeanAll(image);

        }
        public void ScaleMeanAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_Gray Grey)
        {
            
                Skalowanie v = new Skalowanie(skala);
                v.SkalowanieAll(Grey);
          if(skala<0) 
            {

                MeanFilterSmooth1 m = new MeanFilterSmooth1((int)((1.0 / skala) / 2.0));
                m.MaskaAll(Grey);
            }

        }
        public void Visit(image_RGB rgb)
        {
            Skalowanie v = new Skalowanie(skala);
            v.SkalowanieAll(rgb);
            if(skala<0)
            {
                
                MeanFilterSmooth1 m = new MeanFilterSmooth1((int)((1.0/skala)/ 2.0));
                m.MaskaAll(rgb);
            }
        }
    }
}
