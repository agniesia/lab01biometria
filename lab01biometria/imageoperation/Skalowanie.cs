using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Skalowanie:Visitor
    {
        double skala=0;
        public Skalowanie(double skala){
            if(skala<=0)
                throw new BadImputFunkcionExeption();
            this.skala=skala;

        }
        public void rob(image_as_tab image) {
            SkalowanieAll(image);
        }
        public void SkalowanieAll(image_as_tab image)
        {
            image.Accept(this);

        }
        public void Visit(image_Gray Grey){
           
            int newh =(int)(Grey.h *skala);
            int neww =(int)( Grey.w * skala);
            byte[][] NewGrey = new byte[neww][];
            byte[][] Newalfa = new byte[neww][];
            for (int x = 0; x < neww; x++)
            {
                NewGrey[x] = new byte[newh];
                Newalfa[x] = new byte[newh];
                for (int y = 0; y < newh; y++)
                {
                    NewGrey[x][y] = Grey.Greycanal[(int)(x / skala)][(int)(y / skala)];
                    Newalfa[x][y] = Grey.alfa[(int)(x / skala)][(int)(y / skala)];
                }
            }
            //uwaga na resztki 
            Grey.Greycanal =(byte[][]) NewGrey.Clone();
            Grey.alfa = (byte[][])Newalfa.Clone();
            Grey.h = newh;
            Grey.w = neww;


        }
        public void Visit(image_RGB rgb)
        {
            int newh =(int) (rgb.h *skala);
            int neww = (int)(rgb.w * skala);
            byte[][] NewR = new byte[neww][];
            byte[][] NewG = new byte[neww][];
            byte[][] NewB = new byte[neww][];
            byte[][] Newalfa = new byte[neww][];
            for (int x = 0; x < neww; x++)
            {
                NewR[x] = new byte[newh];
                NewG[x] = new byte[newh];
                NewB[x] = new byte[newh];
                Newalfa[x] = new byte[newh];
                for (int y = 0; y < newh; y++)
                {
                    NewR[x][y] = rgb.R[(int)(x / skala)][(int)(y / skala)];
                    NewG[x][y] = rgb.G[(int)(x / skala)][(int)(y / skala)];
                    NewB[x][y] = rgb.B[(int)(x / skala)][(int)(y / skala)];
                    Newalfa[x][y] = rgb.alfa[(int)(x / skala)][(int)(y / skala)];
                }
            }
            //uwaga na resztki 
            rgb.R=(byte[][])NewR.Clone();
            rgb.G=(byte[][])NewG.Clone();
            rgb.B=(byte[][])NewB.Clone();
            
            rgb.alfa = (byte[][])Newalfa.Clone();
            rgb.h = newh;
            rgb.w = neww;

        }
    }
}
