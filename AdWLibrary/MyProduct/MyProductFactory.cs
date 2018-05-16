using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWLibrary.MyProduct
{
    public static class MyProductFactory
    {
        
        public static List<MyProduct> GetProductsByName(string namePart)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var products = DataManager.GetProductsByName(namePart);
                List<MyProduct> myProducts = new List<MyProduct>();
                foreach (var product in products)
                {
                    myProducts.Add(CreateMyProduct(product));
                }
                return myProducts;
            }
        }

        public static List<MyProduct> GetProductsWithNRecentReviews(int howManyReviews)
        {
            var productsWithNReviews = DataManager.GetProductsWithNRecentReviews(howManyReviews);
            List<MyProduct> myProducts = new List<MyProduct>();
            foreach (var product in productsWithNReviews)
            {
                myProducts.Add(CreateMyProduct(product));
            }
            return myProducts;
        }

        public static List<MyProduct> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var products = DataManager.GetNRecentlyReviewedProducts(howManyProducts);
                List<MyProduct> myProducts = new List<MyProduct>();
                foreach (var product in products)
                {
                    myProducts.Add(CreateMyProduct(product));
                }
                return myProducts;
            }
        }

        private static MyProduct CreateMyProduct(Product product)
        {
            if (product == null)
            {
                throw new ProductionArgumentNullException(typeof(Product).ToString());
            }
            MyProduct myProduct = new MyProduct(product.ProductID)
            {
                Name = product.Name,
                StandardCost = product.StandardCost,
                Color = product.Color,
                Size = product.Size,
            };
            return myProduct;
        }
    }
}
