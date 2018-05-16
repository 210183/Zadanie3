using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWLibrary.MyProduct
{
    public class MyProductReview
    {
        public int MyProductReviewId { get; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }

        internal MyProductReview(int Id)
        {
            MyProductReviewId = Id;
        }
    }
}
