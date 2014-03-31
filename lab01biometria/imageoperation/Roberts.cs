using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Roberts:Visitor
    {
        public void RobertsAll(image_as_tab image) {
            image.Accept(this);
        }
        public void rob(image_as_tab image) {
            RobertsAll(image);
        }
        public void Visit(image_RGB rgb){
            image_RGB temp = new image_RGB(rgb.utab, rgb.w, rgb.h);
            for (int i = 0; i < rgb.w - 1; i++)
            {
                for (int j = 0; j < rgb.h - 1; j++)
                {
                    //|p(x,y) - p(x+1,y+1)|+|p(x+1,y)-p(x,y+1)|.
                    temp.R[i][j] = (byte)(Math.Abs(rgb.R[i][j] - rgb.R[i + 1][j + 1]) + Math.Abs(rgb.R[i + 1][j] - rgb.R[i][j + 1]));
                    temp.B[i][j] = (byte)(Math.Abs(rgb.B[i][j] - rgb.B[i + 1][j + 1]) + Math.Abs(rgb.B[i + 1][j] - rgb.B[i][j + 1]));
                    temp.G[i][j] = (byte)(Math.Abs(rgb.G[i][j] - rgb.G[i + 1][j + 1]) + Math.Abs(rgb.G[i + 1][j] - rgb.G[i][j + 1]));


                }


            }
            for (int j = 0; j < rgb.h; j++)
            {
                temp.R[rgb.w - 1][j] = temp.R[rgb.w - 2][j];
                temp.B[rgb.w - 1][j] = temp.B[rgb.w - 2][j];
                temp.G[rgb.w - 1][j] = temp.G[rgb.w - 2][j];
            }
            for (int j = 0; j < rgb.w; j++)
            {
                temp.R[j][rgb.h - 1] = temp.R[j][rgb.h - 2];
                temp.B[j][rgb.h - 1] = temp.B[j][rgb.h - 2];
                temp.G[j][rgb.h - 1] = temp.G[j][rgb.h - 2];
            }
            rgb.R = (byte[][])temp.R.Clone();
            rgb.G = (byte[][])temp.G.Clone();
            rgb.B = (byte[][])temp.B.Clone();

        }
        public void Visit(image_Gray Grey) {
            image_Gray temp = new image_Gray(Grey.utab, Grey.w, Grey.h);
            for (int i = 0; i < Grey.w - 1; i++)
            {
                for (int j = 0; j < Grey.h - 1; j++)
                {
                    //|p(x,y) - p(x+1,y+1)|+|p(x+1,y)-p(x,y+1)|.
                    temp.Greycanal[i][j] = (byte)(Math.Abs(Grey.Greycanal[i][j] - Grey.Greycanal[i + 1][j + 1]) + Math.Abs(Grey.Greycanal[i + 1][j] - Grey.Greycanal[i][j + 1]));
                   
                }


            }
            for (int j = 0; j < Grey.h; j++)
            {
                temp.Greycanal[Grey.w - 1][j] = temp.Greycanal[Grey.w - 2][j];
                
            }
            for (int j = 0; j < Grey.w; j++)
            {
                temp.Greycanal[j][Grey.h - 1] = temp.Greycanal[j][Grey.h - 2];
      
            }
            Grey.Greycanal= (byte[][])temp.Greycanal.Clone();
           
        }


    }

}
