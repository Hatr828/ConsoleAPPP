using Shop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankTransactionExample
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
      .AddDbContext<ShopContext>()
      .AddScoped<OrderService>()
      .BuildServiceProvider();

            var orderService = serviceProvider.GetService<OrderService>();

            using (var context = serviceProvider.GetService<ShopContext>())
            {
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product { Name = "Laptop", Price = 1000 },
                        new Product { Name = "Mouse", Price = 20 },
                        new Product { Name = "Keyboard", Price = 50 }
                    );
                    context.SaveChanges();
                }
            }

            orderService.AddOrder(new int[] { 1, 2 });

            orderService.ViewOrders();

            orderService.RemoveOrder(1);

            orderService.ViewOrders();
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }

    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class OrderService
    {
        private readonly ShopContext _context;

        public OrderService(ShopContext context)
        {
            _context = context;
        }

        public void AddOrder(int[] productIds)
        {
            var order = new Order { OrderDate = DateTime.Now };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var productId in productIds)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductId = productId
                };
                _context.Add(orderProduct);
            }
            _context.SaveChanges();
        }

        public void RemoveOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("error");
            }
        }

        public void ViewOrders()
        {
            var orders = _context.Orders
                                 .Select(o => new
                                 {
                                     o.OrderId,
                                     o.OrderDate,
                                     Products = o.OrderProducts.Select(op => op.Product.Name).ToList()
                                 })
                                 .ToList();

            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}");
                Console.WriteLine("Products:");
                foreach (var product in order.Products)
                {
                    Console.WriteLine($"- {product}");
                }
                Console.WriteLine();
            }
        }
    }
}
