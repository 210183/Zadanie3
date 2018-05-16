
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWLibrary
{
    public static class DataManager
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var productsByName = db.Products.Where(x => x.Name.Contains(namePart)).ToList();
                return productsByName;
            }
        }
        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var productsByVendorName = from Product p in db.Products
                                           join ProductVendor pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                           join Vendor v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                           where v.Name == vendorName
                                           select p;
                return productsByVendorName.ToList();
            }
        }

        public static List<string> GetProductsNamesByVendorName(string vendorName)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var productsNamesByVendorName = from Product p in db.Products
                                                join ProductVendor pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                                join Vendor v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                                where v.Name == vendorName
                                                select p.Name;
                return productsNamesByVendorName.ToList();
            }
        }

        /// <summary>
        /// Returns first found vendor, ordered by vendor name to ensure more predictable outcome
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public static string GetProductVendorByProductName(string productName)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var productVendorByProductName = from Vendor v in db.Vendors
                                                 join ProductVendor pv in db.ProductVendors on v.BusinessEntityID equals pv.BusinessEntityID
                                                 join Product p in db.Products on pv.ProductID equals p.ProductID
                                                 where p.Name == productName
                                                 orderby v.Name
                                                 select v.Name;

                return productVendorByProductName.FirstOrDefault();
            }
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {

                var productsWithNReviews = from Product p in db.Products
                                           where (
                                            from ProductReview pr in db.ProductReviews 
                                            where pr.ProductID == p.ProductID
                                            select pr.ProductID
                                          ).Count() == howManyReviews
                                          select p;
                return productsWithNReviews.ToList();
            }
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var recentlyReviewed = (from Product p in db.Products
                                       join ProductReview pr in db.ProductReviews on p.ProductID equals pr.ProductID
                                       orderby pr.ReviewDate descending
                                       select p).GroupBy(p => p.ProductID).Select(p => p.First()).Take(howManyProducts);

                return recentlyReviewed.ToList();
            }
        }

        public static List<Product> GetNRecentlyReviewedProductsImperative(int howManyProducts)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var recentlyReviewed = db.Products.Join(db.ProductReviews,
                                        p => p.ProductID,
                                        pr => pr.ProductID,
                                        (p, pr) => new
                                        {
                                            Product = p,
                                            Review = pr
                                        })
                                        .GroupBy(p => p.Product.ProductID)
                                        .OrderByDescending
                                        (
                                            p => p.OrderByDescending(r => r.Review.ReviewDate).First().Review.ReviewDate
                                        )
                                        .Take(howManyProducts)
                                        .Select(pr => pr.First().Product);          

                return recentlyReviewed.ToList();
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {

                var products = from Product p in db.Products
                               join ProductSubcategory ps in db.ProductSubcategories on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                               join ProductCategory pc in db.ProductCategories on ps.ProductCategoryID equals pc.ProductCategoryID
                               where pc.Name == categoryName
                               orderby pc.Name, p.Name
                               select p;
                return products.Take(n).ToList();
            }
        }

        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var productsSum = (from Product p in db.Products
                                   join ProductSubcategory ps in db.ProductSubcategories on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                                   join ProductCategory pc in db.ProductCategories on ps.ProductCategoryID equals pc.ProductCategoryID
                                   where pc.ProductCategoryID == category.ProductCategoryID
                                   select (decimal?)p.StandardCost
                                   ).Sum() ?? 0;

                return Convert.ToInt32(productsSum);
            }
        }

        #region extension methods

        public static List<Product> GetUncategorized(this List<Product> products)
        {
            return products.Where(p => p.ProductSubcategory != null).ToList();
        }

        public static List<Product> GetUncategorizedDeclarative(this List<Product> products)
        {
            var selected = from Product p in products
                           where p.ProductSubcategory != null
                           select p;
            return selected.ToList();
        }

        /// <summary>
        /// TODO: Ask about signature, what should that method do ???
        /// </summary>
        /// <param name="products"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static List<Product> GetPaged(this List<Product> products, int pageSize, int pageNumber) 
        {
            var query = products.Skip(pageSize * (pageNumber)).Take(pageSize);
            return query.ToList();
        }

        public static string GetProductsAndTheirVendors(this List<Product> products)
        {
            using (var db = new AdventureWorksDataContext())
            {
                var productsVendors = (from Vendor v in db.Vendors
                                      join ProductVendor pv in db.ProductVendors on v.BusinessEntityID equals pv.BusinessEntityID
                                      join Product p in db.Products on pv.ProductID equals p.ProductID
                                      where products.Select(p => p.ProductID).Contains(p.ProductID)
                                      select new
                                      {
                                          ProductName = p.Name,
                                          VendorName = v.Name
                                      }).ToList();

                StringBuilder sb = new StringBuilder();
                foreach (var pv in productsVendors)
                {
                    sb.AppendFormat($"{pv.ProductName}-{pv.VendorName}\r\n");
                }
                return sb.ToString();
            }
        }
        #endregion
    }
}
