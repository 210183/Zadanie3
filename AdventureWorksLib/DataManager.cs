
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLibrary
{
    public static class DataManager
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (AdventureWorksDBDataContext db = new AdventureWorksDBDataContext())
            {
                var productsByName = db.Products.Where(x => x.Name.Contains(namePart)).ToList();
                return productsByName;
            }
        }
        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            using (AdventureWorksDBDataContext db = new AdventureWorksDBDataContext())
            {
                var productsByVendorName = from Product p in db.Products
                                           join ProductVendor pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                           join Vendor v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                           where v.Name == vendorName
                                           select p;
                return productsByVendorName.ToList();
            }
        }
    }
}
