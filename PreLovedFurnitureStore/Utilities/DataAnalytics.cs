using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using PreLovedFurnitureStore.Models;

namespace PreLovedFurnitureStore.Utilities
{
    /// <summary>
    /// Provides data analytics and summary insights for the furniture store.
    /// </summary>
    public static class DataAnalytics
    {
        /// <summary>
        /// Calculates total sales from all orders.
        /// </summary>
        public static decimal GetTotalSales(List<string> orders)
        {
            decimal total = 0;
            foreach (var order in orders)
            {
                string[] parts = order.Split('|');
                if (parts.Length >= 7 && decimal.TryParse(parts[6], out decimal amount))
                {
                    total += amount;
                }
            }
            return total;
        }

        /// <summary>
        /// Counts total number of orders.
        /// </summary>
        public static int GetTotalOrders(List<string> orders)
        {
            return orders.Count;
        }

        /// <summary>
        /// Counts total number of unique customers.
        /// </summary>
        public static int GetUniqueCustomerCount(List<string> orders)
        {
            HashSet<string> customers = new HashSet<string>();
            foreach (var order in orders)
            {
                string[] parts = order.Split('|');
                if (parts.Length >= 2)
                {
                    customers.Add(parts[1]);  // Customer name
                }
            }
            return customers.Count;
        }

        /// <summary>
        /// Gets the most popular furniture category across all orders.
        /// </summary>
        public static string GetMostPopularCategory(List<string> orders)
        {
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PreLovedFurnitureStore");

            for (int i = 0; i < orders.Count; i++)
            {
                string[] parts = orders[i].Split('|');
                if (parts.Length >= 1)
                {
                    string itemsFile = Path.Combine(dataDirectory, $"order_{i}_items.txt");
                    if (File.Exists(itemsFile))
                    {
                        string[] itemLines = File.ReadAllLines(itemsFile);
                        foreach (var itemLine in itemLines)
                        {
                            string[] itemParts = itemLine.Split('|');
                            if (itemParts.Length >= 4)
                            {
                                string category = itemParts[3];
                                if (categoryCount.ContainsKey(category))
                                    categoryCount[category]++;
                                else
                                    categoryCount[category] = 1;
                            }
                        }
                    }
                }
            }

            if (categoryCount.Count == 0)
                return "No data available";

            return categoryCount.OrderByDescending(x => x.Value).First().Key;
        }

        /// <summary>
        /// Gets average order value.
        /// </summary>
        public static decimal GetAverageOrderValue(List<string> orders)
        {
            if (orders.Count == 0)
                return 0;

            decimal total = GetTotalSales(orders);
            return total / orders.Count;
        }

        /// <summary>
        /// Identifies returning customers (those with multiple orders).
        /// </summary>
        public static int GetReturningCustomerCount(List<string> orders)
        {
            Dictionary<string, int> customerOrders = new Dictionary<string, int>();
            
            foreach (var order in orders)
            {
                string[] parts = order.Split('|');
                if (parts.Length >= 2)
                {
                    string customerName = parts[1];
                    if (customerOrders.ContainsKey(customerName))
                        customerOrders[customerName]++;
                    else
                        customerOrders[customerName] = 1;
                }
            }

            return customerOrders.Count(x => x.Value > 1);
        }
    }
}
