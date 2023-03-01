using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DekorShakirov.DB
{
    public static class DataAccess
    {
        public static List<Product> GetProducts() => db.connection.Product.ToList();
        public static List<User> GetUsers()
        {
            return db.connection.User.ToList();
        }
        public static User GetUser(string login, string password)
        {
            return GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);
        }
        public static List<Supplier> GetSuppliers()
        {
            return db.connection.Supplier.ToList();
        }
        public static List<Unit> GetUnits()
        {
            return db.connection.Unit.ToList();
        }
        public static List<Category> GetCategoryes()
        {
            return db.connection.Category.ToList();
        }

        public static void SaveProduct(Product product)
        {
            if (product.Id == 0)
                db.connection.Product.Add(product);

            db.connection.SaveChanges();
        }

        static void DeleteProduct(Product product)
        {
            db.connection.Product.Remove(product);
            db.connection.SaveChanges();
        }
    }
}
