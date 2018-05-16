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
        /// Factory stores already loaded products in order to avoid repeated downloading from database
        /// </summary>
        private static List<MyProduct> loadedMyProducts = new List<MyProduct>();

        /// <summary>
        /// Creates new MyProduct instance from values from Product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static MyProduct CreateMyProduct(Product product)
        {
            if (product is null)
            {
                throw new ProductionArgumentNullException(typeof(Product).ToString());
            }
            var loadedMyProduct = loadedMyProducts.Find(p => p.ProductID == product.ProductID);
            if(loadedMyProduct != null)
            {
                return loadedMyProduct;
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
            loadedMyProducts.Add(myProduct);
            return myProduct;
        }

        /// <summary>
        /// Creates list of new MyProduct instances from values from Products.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static List<MyProduct> CreateMyProducts(List<Product> products)
        {
            if (products is null)
            {
                throw new ProductionArgumentNullException(typeof(Product).ToString());
            }
            var myProducts = new List<MyProduct>();
            foreach (var product in products)
            {
                myProducts.Add(CreateMyProduct(product));
            }
            return myProducts;
        }

        /// <summary>
        /// Method to clear list of already loaded products if you want to free space
        /// </summary>
        public static void Reset()
        {
            loadedMyProducts.Clear();
        }
    }
}
