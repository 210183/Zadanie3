﻿
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

        //TODO: czy tu nie powinna byc lista??!!
        public static string GetProductVendorByProductName(string productName)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var productVendorByProductName = from Vendor v in db.Vendors
                                                 join ProductVendor pv in db.ProductVendors on v.BusinessEntityID equals pv.BusinessEntityID
                                                 join Product p in db.Products on pv.ProductID equals p.ProductID
                                                 where p.Name == productName
                                                 select v.Name;

                return productVendorByProductName.First();
            }

        }
        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {

                var productsWithNReviews = from Product p in db.Products
                                          where (
                                            from Product p_in in db.Products
                                            join ProductReview pr in db.ProductReviews on p_in.ProductID equals pr.ProductID
                                            where p_in.ProductID == p.ProductID
                                            select p_in.ProductID
                                          ).Count() == howManyReviews
                                          select p;
                return productsWithNReviews.ToList();
            }


        }


    }
}