using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class RGBtoNaturalGrey:Visitor
    {
        image_Gray GreyElement;
        public void RGBtoNaturalGreyAll(image_as_tab image)
        {
            image.Accept(this);
        }
        public void Visit(image_RGB rgb)
        {
            byte z;
            byte[] temp = new byte[rgb.w * rgb.h * 4];
            for (int i = 0; i < rgb.w; i++)
            {
                for (int j = 0; j < rgb.h; j++)
                {
                    //0.3*R+0.59*G+0.11*B
                    z = (byte)(0.11 * rgb.B[i][j] + 0.59 * rgb.G[i][j] + 0.3 * rgb.R[i][j]);
                    temp[4 * (j * rgb.w + i)] = z;
                    temp[4 * (j * rgb.w + i) + 1] = z;
                    temp[4 * (j * rgb.w + i) + 2] = z;
                    temp[4 * (j * rgb.w + i) + 3] = rgb.alfa[i][j];

                }

            }
            GreyElement =new image_Gray(temp, rgb.w, rgb.h);
        }

        public void Visit(image_Gray Grey)
        {
            GreyElement = Grey;
        }
    }
}
