using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWLibrary.MyProduct
{
    public static class MyProductFactory
    {
        /// <summary>
        /// Creates new MyProduct instance from values from Product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static MyProduct CreateMyProduct(Product product)
        {
            if (product is null)
            {
                throw new ProductionArgumentNullException(product.ToString());
            }
            MyProduct myProduct = new MyProduct(product.ProductID)
            {
                Name = product.Name,
                StandardCost = product.StandardCost,
                Color = product.Color,
                Size = product.Size,
            };
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var reviews = from ProductReview pr in db.ProductReviews
                              where pr.ProductID == myProduct.ProductID
                              select pr;
                myProduct.ProductReviews = MyProductReviewFactory.CreateMyProductReviews(reviews.ToList());
            }
            return myProduct;
        }

        /// <summary>
        /// Creates list of new MyProduct instances from values from Products.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static List<MyProduct> CreateMyProduct(List<Product> products)
        {
            if (products is null)
            {
                throw new ProductionArgumentNullException(products.ToString());
            }
            var myProducts = new List<MyProduct>();
            foreach (var product in products)
            {
                myProducts.Add(CreateMyProduct(product));
            }
            return myProducts;
        }
    }
}
