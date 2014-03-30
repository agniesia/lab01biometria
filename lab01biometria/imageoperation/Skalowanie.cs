using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Skalowanie:Visitor
    {
        int skala=0;
        public Skalowanie(int skala){
            if(skala<=0)
                throw new BadImputFunkcionExeption();
            this.skala=skala;

        }
        public void rob(image_as_tab image) { }
        public void SkalowanieAll(image_as_tab image)
        {
            image.Accept(this);

        }
        public void Visit(image_Gray Grey){
            int newh = Grey.h / skala;
            int neww = Grey.w / skala;
            byte[][] NewGrey = new byte[neww][];
            byte[][] Newalfa = new byte[neww][];
            for (int x = 0; x < neww; x++)
            {
                NewGrey[x] = new byte[newh];
                Newalfa[x] = new byte[newh];
                for (int y = 0; y < newh; y++)
                {
                    NewGrey[x][y] = Grey.Greycanal[x / skala][y / skala];
                    Newalfa[x][y] = Grey.alfa[x / skala][y / skala];
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
            int newh = rgb.h / 2;
            int neww = rgb.w / 2;
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
                    NewR[x][y] = rgb.R[x / skala][y / skala];
                    NewG[x][y] = rgb.G[x / skala][y / skala];
                    NewB[x][y] = rgb.B[x / skala][y / skala];
                    Newalfa[x][y] = rgb.alfa[x / skala][y / skala];
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
