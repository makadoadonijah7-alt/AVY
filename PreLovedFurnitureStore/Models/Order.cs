using System;
using System.Collections.Generic;

namespace PreLovedFurnitureStore.Models
{
    /// <summary>
    /// Represents a customer order containing furniture items and order details.
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryPreference { get; set; }
        public string OrderStatus { get; set; }  // pending, completed, cancelled

        public Order()
        {
            Items = new List<OrderItem>();
            OrderDate = DateTime.Now;
            OrderStatus = "pending";
        }

        public Order(int orderId, int customerId, decimal totalAmount, string deliveryPreference)
        {
            OrderId = orderId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
            DeliveryPreference = deliveryPreference;
            OrderDate = DateTime.Now;
            OrderStatus = "pending";
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
            TotalAmount += item.ItemPrice * item.Quantity;
        }

        public override string ToString()
        {
            return $"{OrderId}|{CustomerId}|{TotalAmount}|{OrderDate}|{DeliveryPreference}|{OrderStatus}";
        }
    }

    /// <summary>
    /// Represents an individual item within an order.
    /// </summary>
    public class OrderItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Material { get; set; }

        public OrderItem(int itemId, string itemName, decimal itemPrice, int quantity, 
                        string category, string material)
        {
            ItemId = itemId;
            ItemName = itemName;
            ItemPrice = itemPrice;
            Quantity = quantity;
            Category = category;
            Material = material;
        }

        public decimal GetSubtotal()
        {
            return ItemPrice * Quantity;
        }
    }
}
