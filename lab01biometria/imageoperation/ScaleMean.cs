using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class ScaleMean : Visitor
    {
        int skala;
        public ScaleMean(int skala)
        {
            if (skala <= 0 || skala > 1)
                throw new BadImputFunkcionExeption();
            this.skala = skala;
        }
        public void ScaleMeanAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_Gray Grey)
        {
            Skalowanie v = new Skalowanie(skala);
            v.SkalowanieAll(Grey);
            MeanFilterSmooth1 m = new MeanFilterSmooth1(skala / 2);
            m.MaskaAll(Grey);

        }
        public void Visit(image_RGB rgb)
        {
            Skalowanie v = new Skalowanie(skala);
            v.SkalowanieAll(rgb);
            MeanFilterSmooth1 m = new MeanFilterSmooth1(skala / 2);
            m.MaskaAll(rgb);
        }
    }
}
