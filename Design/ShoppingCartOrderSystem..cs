using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace Design
    {
        public record Product(int Id, string Name, decimal Price);

        public class CartItem
        {
            public Product Product { get; }
            public int Quantity { get; private set; }

            public decimal SubTotal => Product.Price * Quantity;

            public CartItem(Product product, int quantity)
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));

                if (quantity <= 0)
                    throw new ArgumentException("Quantity must be greater than zero.");

                Product = product;
                Quantity = quantity;
            }

            public void IncreaseQuantity(int amount)
            {
                if (amount <= 0)
                    throw new ArgumentException("Amount must be greater than zero.");

                Quantity += amount;
            }

            public void DecreaseQuantity(int amount)
            {
                if (amount <= 0)
                    throw new ArgumentException("Amount must be greater than zero.");

                if (amount > Quantity)
                    throw new InvalidOperationException("Cannot decrease below zero.");

                Quantity -= amount;
            }
        }

        public class ShoppingCart
        {
            public int CustomerId { get; }

            private readonly List<CartItem> _items = new();
            public IReadOnlyList<CartItem> Items => _items;

            public ShoppingCart(int customerId)
            {
                CustomerId = customerId;
            }

            public void AddProduct(Product product, int quantity)
            {
                var existingItem = _items.FirstOrDefault(i => i.Product.Id == product.Id);

                if (existingItem != null)
                {
                    existingItem.IncreaseQuantity(quantity);
                }
                else
                {
                    _items.Add(new CartItem(product, quantity));
                }
            }

            public void RemoveProduct(int productId, int quantity)
            {
                var existingItem = _items.FirstOrDefault(i => i.Product.Id == productId);
                if (existingItem == null)
                    throw new InvalidOperationException("Product not found in cart.");

                if (quantity >= existingItem.Quantity)
                {
                    _items.Remove(existingItem);
                }
                else
                {
                    existingItem.DecreaseQuantity(quantity);
                }
            }

            public decimal GetTotal()
            {
                return _items.Sum(i => i.SubTotal);
            }

            public bool IsEmpty()
            {
                return !_items.Any();
            }

            public void Clear()
            {
                _items.Clear();
            }
        }

        public record OrderItem(int ProductId, string ProductName, decimal UnitPrice, int Quantity)
        {
            public decimal SubTotal => UnitPrice * Quantity;
        }

        public class Order
        {
            public int Id { get; }
            public int CustomerId { get; }
            public DateTime CreatedAt { get; }
            public IReadOnlyList<OrderItem> Items { get; }
            public decimal TotalAmount { get; }

            public Order(int id, int customerId, List<OrderItem> items)
            {
                Id = id;
                CustomerId = customerId;
                CreatedAt = DateTime.Now;
                Items = items.AsReadOnly();
                TotalAmount = Items.Sum(i => i.SubTotal);
            }
        }

        public class Store
        {
            private readonly List<Product> _products = new();
            private int _nextOrderId = 1;

            public void AddProduct(Product product)
            {
                if (_products.Any(p => p.Id == product.Id))
                    throw new InvalidOperationException("Product with same Id already exists.");

                _products.Add(product);
            }

            public Product GetProductById(int productId)
            {
                var product = _products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                    throw new InvalidOperationException("Product not found.");

                return product;
            }

            public Order Checkout(ShoppingCart cart)
            {
                if (cart == null)
                    throw new ArgumentNullException(nameof(cart));

                if (cart.IsEmpty())
                    throw new InvalidOperationException("Cannot checkout an empty cart.");

                var orderItems = cart.Items
                    .Select(i => new OrderItem(
                        i.Product.Id,
                        i.Product.Name,
                        i.Product.Price,
                        i.Quantity))
                    .ToList();

                var order = new Order(_nextOrderId++, cart.CustomerId, orderItems);

                cart.Clear();

                return order;
            }
        }

        public class Program
        {
            public static void Test()
            {
                var store = new Store();

                store.AddProduct(new Product(1, "Laptop", 1200m));
                store.AddProduct(new Product(2, "Mouse", 25m));
                store.AddProduct(new Product(3, "Keyboard", 75m));

                var cart = new ShoppingCart(customerId: 101);

                cart.AddProduct(store.GetProductById(1), 1);
                cart.AddProduct(store.GetProductById(2), 2);
                cart.AddProduct(store.GetProductById(3), 1);

                Console.WriteLine($"Cart total: {cart.GetTotal()}");

                var order = store.Checkout(cart);

                Console.WriteLine($"Order Id: {order.Id}");
                Console.WriteLine($"Customer Id: {order.CustomerId}");
                Console.WriteLine($"Order total: {order.TotalAmount}");
                Console.WriteLine($"Created at: {order.CreatedAt}");
            }
        }
    }
}
