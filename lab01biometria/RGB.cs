using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria
{
    class image_RGB : image_as_tab
    {
        public byte[][] B;
        public byte[][] G;
        public byte[][] R;
        public byte[][] alfa;
        public byte[] RCanal;
        public byte[] GCanal;
        public byte[] BCanal;

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }

        public image_RGB(byte[] sourcePixels, int wight, int hight)
            : base(sourcePixels, wight, hight)
        {


            byte[] RCanal = utab.Where((x, i) => i % 4 == 0).ToArray();
            byte[] BCanal = utab.Where((x, i) => i % 4 == 1).ToArray();
            byte[] GCanal = utab.Where((x, i) => i % 4 == 2).ToArray();

            int k = 0;
            w = wight;
            h = hight;

            B = new byte[wight][];
            G = new byte[wight][];
            R = new byte[wight][];
            alfa = new byte[wight][];
            for (int i = 0; i < wight; i++)
            {
                B[i] = new byte[hight];
                G[i] = new byte[hight];
                R[i] = new byte[hight];
                alfa[i] = new byte[hight];
                for (int j = 0; j < hight; j++)
                {

                    k = 4 * (j * w + i);
                    B[i][j] = sourcePixels[k];
                    G[i][j] = sourcePixels[k + 1];
                    R[i][j] = sourcePixels[k + 2];
                    alfa[i][j] = sourcePixels[k + 3];

                }
            }
        }

        public override byte[] show()
        {
            return imagearray3Dto1D();
        }
        public override void negative()
        {
            base.negative();
            int k = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {

                    k = 4 * (j * w + i);
                    B[i][j] = utab[k];
                    G[i][j] = utab[k + 1];
                    R[i][j] = utab[k + 2];
                    alfa[i][j] = utab[k + 3];

                }
            }
        }



        public byte[] imagearray3Dto1D()
        {

            byte[] temp = new byte[w * h * 4];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {

                    temp[4 * (j * w + i)] = B[i][j];
                    temp[4 * (j * w + i) + 1] = G[i][j];
                    temp[4 * (j * w + i) + 2] = R[i][j];
                    temp[4 * (j * w + i) + 3] = alfa[i][j];


                }
            }
            return temp;
        }

        public void normalize()
        {
            //max
            var maxR = R.SelectMany(x => x).ToArray().Max();
            var maxB = B.SelectMany(x => x).ToArray().Max();
            var maxG = G.SelectMany(x => x).ToArray().Max();
            //min
            var minG = G.SelectMany(x => x).ToArray().Min();
            var minB = B.SelectMany(x => x).ToArray().Min();
            var minR = R.SelectMany(x => x).ToArray().Min();
            var normalizeConstR = maxR - minR;
            var normalizeConstG = maxR - minG;
            var normalizeConstB = maxR - minB;


            ///pixel[x,y]=255*(pixel[x,y]-minPix)/(maxPix-minPix) 

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    R[i][j] = (byte)(255 * (R[i][j] - minR) / normalizeConstR);
                    G[i][j] = (byte)(255 * (G[i][j] - minG) / normalizeConstG);
                    B[i][j] = (byte)(255 * (B[i][j] - minB) / normalizeConstB);
                }
            }






        }
        public image_Gray greyimage()
        {
            byte z;
            byte[] temp = new byte[w * h * 4];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    z = (byte)((this.B[i][j] + this.G[i][j] + this.R[i][j]) / 3);
                    temp[4 * (j * w + i)] = z;
                    temp[4 * (j * w + i) + 1] = z;
                    temp[4 * (j * w + i) + 2] = z;
                    temp[4 * (j * w + i) + 3] = alfa[i][j];
                }

            }
            return new image_Gray(temp, w, h);

        }

        public image_Gray grey_naturalimage()
        {
            byte z;
            byte[] temp = new byte[w * h * 4];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    //0.3*R+0.59*G+0.11*B
                    z = (byte)(0.11 * this.B[i][j] + 0.59 * this.G[i][j] + 0.3 * this.R[i][j]);
                    temp[4 * (j * w + i)] = z;
                    temp[4 * (j * w + i) + 1] = z;
                    temp[4 * (j * w + i) + 2] = z;
                    temp[4 * (j * w + i) + 3] = alfa[i][j];

                }

            }
            return new image_Gray(temp, w, h);

        }

        public void sepia(byte Waga)
        {


            byte[] temp = new byte[w * h * 4];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    //R=R+2*W, G=G+W, B=B

                    if (G[i][j] + Waga <= 255)
                        G[i][j] = (byte)(G[i][j] + Waga);
                    else
                        G[i][j] = 255;
                    if (R[i][j] + 2 * Waga <= 255)
                        R[i][j] = (byte)(R[i][j] + 2 * Waga);
                    else
                        R[i][j] = 255;

                }

            }


        }
        public void sobel()
        {
            Sobel Sobeltemp = new Sobel();
            Sobeltemp.Convolve(this);
        }

        public image_RGB Roberts()
        {
            image_RGB temp = new image_RGB(this.utab, w, h);
            for (int i = 0; i < w - 1; i++)
            {
                for (int j = 0; j < h - 1; j++)
                {
                    //|p(x,y) - p(x+1,y+1)|+|p(x+1,y)-p(x,y+1)|.
                    temp.R[i][j] = (byte)(Math.Abs(R[i][j] - R[i + 1][j + 1]) + Math.Abs(R[i + 1][j] - R[i][j + 1]));
                    temp.B[i][j] = (byte)(Math.Abs(B[i][j] - B[i + 1][j + 1]) + Math.Abs(B[i + 1][j] - B[i][j + 1]));
                    temp.G[i][j] = (byte)(Math.Abs(G[i][j] - G[i + 1][j + 1]) + Math.Abs(G[i + 1][j] - G[i][j + 1]));


                }


            }
            for (int j = 0; j < h; j++)
            {
                temp.R[w - 1][j] = temp.R[w - 2][j];
                temp.B[w - 1][j] = temp.B[w - 2][j];
                temp.G[w - 1][j] = temp.G[w - 2][j];
            }
            for (int j = 0; j < w; j++)
            {
                temp.R[j][h - 1] = temp.R[j][h - 2];
                temp.B[j][h - 1] = temp.B[j][h - 2];
                temp.G[j][h - 1] = temp.G[j][h - 2];
            }
            return temp;
        }
        public image_RGB histogram()
        {
            int[] Hist = new int[256];
            int[] HistR = new int[256];
            int[] HistG = new int[256];
            int[] HistB = new int[256];
            for (int k = 0; k < 256; k++)
            {
                HistR[k] = R.SelectMany(x => x).ToArray().Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
                HistG[k] = G.SelectMany(x => x).ToArray().Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
                HistB[k] = B.SelectMany(x => x).ToArray().Where((x, i) => i % 4 == 0 && x == k).ToArray().Length;
            }
            Hist = HistR.Select((x, i) => x = (x + HistG[i] + HistB[i]) / 3).ToArray();
            int max = Hist.Max();
            for (int i = 0; i < 256; i++)
            {
                Hist[i] = 180 * Hist[i] / max;
            }

            byte[][] histobraz = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                histobraz[i] = new byte[180];

                for (int j = 0; j < 180; j++)
                {
                    if (j < (180 - Hist[i]))
                        histobraz[i][j] = 0;
                    else
                        histobraz[i][j] = 100;
                }
            }
            byte[] temp = new byte[256 * 180 * 4];

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 180; j++)
                {

                    temp[4 * (j * 256 + i)] = histobraz[i][j];
                    temp[4 * (j * 256 + i) + 1] = histobraz[i][j];
                    temp[4 * (j * 256 + i) + 2] = histobraz[i][j];
                    temp[4 * (j * 256 + i) + 3] = 255;


                }
            }

            return new image_RGB(temp, 256, 180);
            //byte[] histobraz = new byte[184320];


            //for(int i=0;i<180;i++){

            //    for (int e = 0; e < 256; e++)
            //    {
            //        if (i < 180 - Hist[e]){
            //            histobraz[4*(i*256+e)] = 60;
            //            histobraz[4*(i*256+e)+1] = 20;
            //            histobraz[4*(i*256+e)+2] = 15;
            //            histobraz[4*(i*256+e)+3] = 255; }


            //        else{
            //            histobraz[4*(i*256+e)] = 20;
            //            histobraz[4*(i*256+e)+1] = 100;
            //            histobraz[4*(i*256+e)+2] = 255;
            //            histobraz[4*(i*256+e)+3] = 255;    
            //    }}


            //}
            //return histobraz;
        }
        public void noisegeneratorSoliPieprz(int chance){

            var zakres=w*h*chance/100;
            Random rnd = new Random(); 
            byte[] IndexRandom=new byte[zakres];
            for (int i=0; i < zakres; i++)
            {
                var indexX = rnd.Next(R.GetLength(0));
                var indexY = rnd.Next(R.GetLength(1));
                if (rnd.Next(0, 2)==0){
                    R[indexX][indexY] = 255;
                    G[indexX][indexY] = 255;
                    B[indexX][indexY] = 255;
                }
                else
                {
                    R[indexX][indexY] = 0;
                    G[indexX][indexY] = 0;
                    B[indexX][indexY] = 0;

                }
            }

            
        }
        public void noisegeneratorRownomienySameCalanl(int chance, byte zakres1, byte zakres2)
        {
            var zakres = w * h * chance / 100;
            Random rnd = new Random();
            byte[] IndexRandom = new byte[zakres];
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(R.GetLength(0));
                var indexY = rnd.Next(R.GetLength(1));

                byte szum =(byte)rnd.Next(zakres1,zakres2);
                if (rnd.Next(0, 2) == 0)
                {
                    R[indexX][indexY] -= szum;
                    G[indexX][indexY] -= szum;
                    B[indexX][indexY] -= szum;
                }
                else
                {
                    R[indexX][indexY] += szum;
                    G[indexX][indexY] += szum;
                    B[indexX][indexY] += szum;

                }

            }
            //NORMALIZCJA (napisz funkcje)
        }
        public  void noisegeneratorRownomienyDifferCalanl(int chance, byte zakres1, byte zakres2){
            var zakres = w * h * chance / 100;
            Random rnd = new Random();
            byte[] IndexRandom = new byte[zakres];
            for (int i = 0; i < zakres; i++)
            {
                var indexX = rnd.Next(R.GetLength(0));
                var indexY = rnd.Next(R.GetLength(1));

                var szum1 = rnd.Next(zakres1, zakres2+1) * Math.Pow(-1, rnd.Next(0, 2));
                var szum2 = rnd.Next(zakres1, zakres2+1) * Math.Pow(-1, rnd.Next(0, 2));
                var szum3 = rnd.Next(zakres1, zakres2+1) * Math.Pow(-1, rnd.Next(0, 2));
           
                //zmienić typ b w sumie potem nie potrzebnie rzutuje a mozna zrobic rzutowanie dopiero przy wyswietlanie
                //R[indexX][indexY] += szum1;
                //G[indexX][indexY] += szum2;
                //B[indexX][indexY] += szum3;
            }

        }
        public void MedianFilter(int rozmiar){
            int[,] TempR = new int[w, h];
            int[,] TempG = new int[w, h];
            int[,] TempB = new int[w, h];



        }




    }
}
