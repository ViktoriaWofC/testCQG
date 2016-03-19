using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TestApplication
{
    public class ButImage
    {
        public bool open = false;
        public int index;
        public static int nn = 0;
        public string imagePath;
        public int hashBut;

        public ButImage() { }


        public void setIndexImag(int i, string path)
        {
            this.index = i;
            this.imagePath = path + i + ".jpg";
        }        
                
    }
}
