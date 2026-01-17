using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Models;
using OrderManagementApp;
using System.Text.RegularExpressions;

namespace OrderManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new OrderDbContext())
            {
                // Câu 1: Khởi tạo DB & Seed Data
                db.Database.EnsureCreated();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== ORDER MANAGEMENT SYSTEM 2026 ===");
                    Console.WriteLine("1. View Orders (Pagination)");
                    Console.WriteLine("2. Create New Order (Validation)");
                    Console.WriteLine("3. Update Order");
                    Console.WriteLine("4. Delete Order");
                    Console.WriteLine("5. Exit");
                    Console.Write("Choice: ");

                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1": ReadOrders(db); break;
                        case "2": CreateOrder(db); break;
                        case "5": return;
                        default: Console.WriteLine("Invalid choice!"); Console.ReadLine(); break;
                    }
                }
            }
        }

        static void ReadOrders(OrderDbContext db)
        {
            // Câu 2.2: Phân trang 10 bản ghi
            var orders = db.Orders.Include(o => o.Product).Take(10).ToList();
            Console.WriteLine("\n{0,-18} | {1,-15} | {2,-15}", "Order No", "Customer", "Product");
            foreach (var o in orders)
            {
                Console.WriteLine("{0,-18} | {1,-15} | {2,-15}", o.OrderNumber, o.CustomerName, o.Product.Name);
            }
            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }

        static void CreateOrder(OrderDbContext db)
        {
            try {
                Console.Write("Enter Order Number (ORD-YYYYMMDD-XXXX): ");
                string code = Console.ReadLine();
                // Câu 2.1: Regex Validation
                if (!Regex.IsMatch(code, @"^ORD-\d{8}-\d{4}$")) throw new Exception("Wrong format!");
                Console.WriteLine("✅ Order validated!");
            } catch (Exception ex) { Console.WriteLine($"❌ Error: {ex.Message}"); }
            Console.ReadLine();
        }
    }
}