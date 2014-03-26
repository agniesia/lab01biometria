using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01biometria.imageoperation
{
    class Roate:Visitor
    {
        int phi;
        public Roate(int phi)
        {
            this.phi = phi;

        }
        public void  RoateAll(image_as_tab image){
            image.Accept(this);
        }
        public void  Visitor(image_Gray Grey){

        }
    }
}
