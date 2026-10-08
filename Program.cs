using Microsoft.EntityFrameworkCore;

namespace HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new ShopContext();
            var t1 = db.Products.Where(p => p.CategoryId == 2).ToList();
            var t2 = db.Products.Where(p => p.Price >= 1000 && p.Price <= 5000).ToList();
            var t3 = db.Products.Where(p => p.Stock < 20 && p.Stock > 0).ToList();
            var t4 = db.Customers.Where(c => c.RegisteredAt.Year == 2025).ToList();
            var t5 = db.Customers.Where(c => c.City == "Казань" || c.City == "Новосибирск").ToList();
            var t6 = db.Customers.Where(c => c.City != "Москва").ToList();
            var t7 = db.Orders.Where(o => o.Status == "new").ToList();
            var t8 = db.Orders.Where(o => o.OrderDate > new DateTime(2025, 6, 1)).ToList();
            var t9 = db.Orders.Where(o =>
                o.OrderDate.DayOfWeek == DayOfWeek.Saturday ||
                o.OrderDate.DayOfWeek == DayOfWeek.Sunday).ToList();
            var t10 = db.Products.Where(p => EF.Functions.ILike(p.Name, "%книга%")).ToList();
            var t11 = db.Customers.Where(c => c.FullName.StartsWith("А")).ToList();
            var t12 = db.OrderItems.Where(oi => oi.Quantity > 1).ToList();
            var prices = new[] { 990m, 1490m };
            var t13 = db.Products.Where(p => prices.Contains(p.Price)).ToList();
            var t14 = db.Products.OrderBy(p => p.Name).ToList();
            var t15 = db.Products.OrderBy(p => p.Stock).ThenByDescending(p => p.Price).ToList();
            var t16 = db.Customers.OrderByDescending(c => c.RegisteredAt).ToList();
            var t17 = db.Customers.OrderBy(c => c.City).ThenBy(c => c.FullName).ToList();
            var t18 = db.Orders
                .OrderBy(o => o.OrderDate)
                .Select(o => new { o.Id, o.OrderDate })
                .ToList();
            var t19 = db.Products.OrderByDescending(p => p.Price).First();
            var t20 = db.Products.FirstOrDefault(p => p.Id == 100);
            Console.WriteLine(t20 is null ? "Товар не найден (null)" : t20.Name);
            try
            {
                var t21 = db.Customers.Single(c => c.Email == "oleg@mail.ru");
                Console.WriteLine($"t21: {t21.FullName}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"t21 ошибка: {ex.Message}");
            }
            var t22 = db.Orders.OrderBy(o => o.OrderDate).First();
            var t23 = db.Customers.OrderBy(c => c.RegisteredAt).Last();
            var t24 = db.Products.Find(5);
            var t25 = db.Products.Any(p => p.Price > 100_000);
            var t26 = db.Products.All(p => p.Price > 500);
            var t27 = db.Customers.Any(c => c.City == "Сочи");
            var allowed = new[] { "new", "paid", "shipped", "cancelled" };
            var t28 = db.Orders.All(o => allowed.Contains(o.Status));
            var totalCustomers = db.Customers.Count();
            var moscowCustomers = db.Customers.Count(c => c.City == "Москва");
            var minPrice = db.Products.Min(p => p.Price);
            var maxPrice = db.Products.Max(p => p.Price);
            var avgPrice = db.Products.Average(p => p.Price);
            var totalStock = db.Products.Sum(p => p.Stock);
            var totalSold = db.OrderItems.Sum(oi => oi.Quantity);
            var t33 = db.Products.OrderByDescending(p => p.Price).Take(5).ToList();
            var t34 = db.Products.OrderBy(p => p.Price).Skip(3).ToList();
            var t35 = db.Products.OrderBy(p => p.Name).Skip(4).Take(4).ToList();

            var query36 = db.Products.Where(p => p.Price > 5000);
            Console.WriteLine("До ToList SQL ещё не отправлен");
            var list36 = query36.ToList();
            Console.WriteLine($"36: {list36.Count} товаров");

            var has37a = db.Products.Count() > 0;
            var has37b = db.Products.Any();

            var q2 = (from p in db.Products
                      where p.Price >= 1000 && p.Price <= 5000
                      select p).ToList();

            var q5 = (from c in db.Customers
                      where c.City == "Казань" || c.City == "Новосибирск"
                      select c).ToList();

            var q14 = (from p in db.Products
                       orderby p.Name
                       select p).ToList();

            var q39a = db.Products.Where(p => p.CategoryId == 2).ToList();
            var q39b = db.Products.AsEnumerable().Where(p => p.CategoryId == 2).ToList();

            Console.WriteLine("Готово");
        }
    }
}
