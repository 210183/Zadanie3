using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWLibrary.MyProduct
{
    public static class MyProductReviewFactory
    {
        public static MyProductReview CreateMyProductReview(ProductReview productReview)
        {
            if (productReview is null)
            {
                throw new ProductionArgumentNullException(productReview.ToString());
            }
            MyProductReview myProductReview = new MyProductReview(productReview.ProductReviewID)
            {
                ReviewDate = productReview.ReviewDate,
                Rating = productReview.Rating
            };
            return myProductReview;
        }

        public static List<MyProductReview> CreateMyProductReviews(List<ProductReview> productReviews)
        {
            if (productReviews is null)
            {
                throw new ProductionArgumentNullException(productReviews.ToString());
            }
            var myProductReviews = new List<MyProductReview>();
            foreach (var review in productReviews)
            {
                myProductReviews.Add(CreateMyProductReview(review));
            }
            return myProductReviews;
        }
    }
}
