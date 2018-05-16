using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWLibrary.MyProduct
{
    public class ProductionArgumentNullException : ArgumentNullException
    {
        public ProductionArgumentNullException(string paramName) : base(paramName)
        {
        }
    }
}
