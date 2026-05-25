using System;
using System.IO;
using System.Collections.Generic;
using PreLovedFurnitureStore.Models;

namespace PreLovedFurnitureStore.Utilities
{
    /// <summary>
    /// Handles file operations for storing and retrieving customer and order data.
    /// Uses text files for data persistence.
    /// </summary>
    public static class FileHandler
    {
        private static readonly string DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PreLovedFurnitureStore");
        private static readonly string OrdersFilePath = Path.Combine(DataDirectory, "orders.txt");
        private static readonly string CustomersFilePath = Path.Combine(DataDirectory, "customers.txt");
        private static readonly string FurnitureFilePath = Path.Combine(DataDirectory, "furniture.txt");

        static FileHandler()
        {
            // Ensure data directory exists
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }

        /// <summary>
        /// Saves an order to the orders file.
        /// </summary>
        public static void SaveOrder(Order order, Customer customer, List<OrderItem> items)
        {
            try
            {
                string orderLine = $"{order.OrderId}|{customer.FullName}|{customer.Email}|{customer.PhoneNumber}|{customer.Address}|{order.DeliveryPreference}|{order.TotalAmount}|{order.OrderDate}";
                
                File.AppendAllText(OrdersFilePath, orderLine + Environment.NewLine);

                // Save items for this order
                string itemsFile = Path.Combine(DataDirectory, $"order_{order.OrderId}_items.txt");
                foreach (var item in items)
                {
                    File.AppendAllText(itemsFile, $"{item.ItemName}|{item.ItemPrice}|{item.Quantity}|{item.Category}|{item.Material}" + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving order: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all orders from the file.
        /// </summary>
        public static List<string> GetAllOrders()
        {
            try
            {
                if (!File.Exists(OrdersFilePath))
                    return new List<string>();

                return new List<string>(File.ReadAllLines(OrdersFilePath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving orders: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves order items for a specific order.
        /// </summary>
        public static List<string> GetOrderItems(int orderId)
        {
            try
            {
                string itemsFile = Path.Combine(DataDirectory, $"order_{orderId}_items.txt");
                if (!File.Exists(itemsFile))
                    return new List<string>();

                return new List<string>(File.ReadAllLines(itemsFile));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving order items: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves a customer record.
        /// </summary>
        public static void SaveCustomer(Customer customer)
        {
            try
            {
                string customerLine = customer.ToString();
                File.AppendAllText(CustomersFilePath, customerLine + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving customer: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all customer records.
        /// </summary>
        public static List<string> GetAllCustomers()
        {
            try
            {
                if (!File.Exists(CustomersFilePath))
                    return new List<string>();

                return new List<string>(File.ReadAllLines(CustomersFilePath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customers: {ex.Message}");
            }
        }
    }
}
