using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWLibrary.MyProduct
{
    public class MyProduct
    {
        public int ProductID { get; }
        public string Name { get; set; }
        public decimal StandardCost { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        internal MyProduct(int Id)
        {
            ProductID = Id;
        }
    }
}
