using System;
using System.Collections.Generic;
using System.Linq;

namespace Design
{
    public record MenuItem(int Id, string Name, decimal Price);

    public class OrderItem
    {
        public MenuItem MenuItem { get; }
        public int Quantity { get; private set; }

        public decimal SubTotal => MenuItem.Price * Quantity;

        public OrderItem(MenuItem menuItem, int quantity)
        {
            MenuItem = menuItem;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }
    }

    public class Order
    {
        public int Id { get; }
        public int TableNumber { get; }
        public bool IsCompleted { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyList<OrderItem> Items => _items;

        public Order(int id, int tableNumber)
        {
            Id = id;
            TableNumber = tableNumber;
        }

        public void AddItem(MenuItem menuItem, int quantity)
        {
            var existingItem = _items.FirstOrDefault(i => i.MenuItem.Id == menuItem.Id);

            if (existingItem != null)
                existingItem.IncreaseQuantity(quantity);
            else
                _items.Add(new OrderItem(menuItem, quantity));
        }

        public decimal GetTotal() => _items.Sum(i => i.SubTotal);

        public void CompleteOrder() => IsCompleted = true;
    }

    public class Table
    {
        public int Number { get; }
        public Order? ActiveOrder { get; private set; }

        public Table(int number)
        {
            Number = number;
        }

        public void AssignOrder(Order order) => ActiveOrder = order;

        public void ClearTable() => ActiveOrder = null;
    }

    public class Restaurant
    {
        private readonly List<MenuItem> _menu;
        private readonly List<Table> _tables;
        private int _nextOrderId = 1;

        public Restaurant(List<MenuItem> menu, List<Table> tables)
        {
            _menu = menu;
            _tables = tables;
        }

        public void CreateOrder(int tableNumber)
        {
            var table = _tables.First(t => t.Number == tableNumber);
            table.AssignOrder(new Order(_nextOrderId++, tableNumber));
        }

        public void AddItemToOrder(int tableNumber, int menuItemId, int quantity)
        {
            var table = _tables.First(t => t.Number == tableNumber);
            var menuItem = _menu.First(m => m.Id == menuItemId);
            table.ActiveOrder!.AddItem(menuItem, quantity);
        }

        public decimal GetBill(int tableNumber)
        {
            var table = _tables.First(t => t.Number == tableNumber);
            return table.ActiveOrder!.GetTotal();
        }

        public void CompleteOrder(int tableNumber)
        {
            var table = _tables.First(t => t.Number == tableNumber);
            table.ActiveOrder!.CompleteOrder();
            table.ClearTable();
        }
    }
}