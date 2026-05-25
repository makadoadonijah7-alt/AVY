using System;
using System.Collections.Generic;
using System.IO;
using PreLovedFurnitureStore.Models;

namespace PreLovedFurnitureStore.Utilities
{
    /// <summary>
    /// Generates and manages invoice documents for orders.
    /// </summary>
    public static class InvoiceGenerator
    {
        /// <summary>
        /// Generates a formatted invoice string for a given order.
        /// </summary>
        public static string GenerateInvoice(int invoiceNumber, Customer customer, List<OrderItem> items, 
                                            decimal totalAmount, string deliveryPreference)
        {
            string invoiceContent = "";

            invoiceContent += "=============================================================" + Environment.NewLine;
            invoiceContent += "                  PRE-LOVED FURNITURE STORE" + Environment.NewLine;
            invoiceContent += "                        INVOICE" + Environment.NewLine;
            invoiceContent += "=============================================================" + Environment.NewLine;
            invoiceContent += Environment.NewLine;

            invoiceContent += $"Invoice Number: {invoiceNumber:D6}" + Environment.NewLine;
            invoiceContent += $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}" + Environment.NewLine;
            invoiceContent += Environment.NewLine;

            invoiceContent += "-------------------------------------------------------------" + Environment.NewLine;
            invoiceContent += "CUSTOMER DETAILS" + Environment.NewLine;
            invoiceContent += "-------------------------------------------------------------" + Environment.NewLine;
            invoiceContent += $"Name: {customer.FullName}" + Environment.NewLine;
            invoiceContent += $"Email: {customer.Email}" + Environment.NewLine;
            invoiceContent += $"Phone: {customer.PhoneNumber}" + Environment.NewLine;
            invoiceContent += $"Address: {customer.Address}" + Environment.NewLine;
            invoiceContent += $"Delivery Method: {deliveryPreference}" + Environment.NewLine;
            invoiceContent += Environment.NewLine;

            invoiceContent += "-------------------------------------------------------------" + Environment.NewLine;
            invoiceContent += "ITEMS PURCHASED" + Environment.NewLine;
            invoiceContent += "-------------------------------------------------------------" + Environment.NewLine;
            invoiceContent += string.Format("{0,-40} {1,10} {2,10} {3,10}", "Item Name", "Price", "Qty", "Subtotal") + Environment.NewLine;
            invoiceContent += "-------------------------------------------------------------" + Environment.NewLine;

            foreach (var item in items)
            {
                decimal subtotal = item.GetSubtotal();
                invoiceContent += string.Format("{0,-40} ${1,9:F2} {2,10} ${3,9:F2}", 
                                               item.ItemName, item.ItemPrice, item.Quantity, subtotal) + Environment.NewLine;
            }

            invoiceContent += "-------------------------------------------------------------" + Environment.NewLine;
            invoiceContent += string.Format("{0,-40} {1,10} {2,10} ${3,9:F2}", "", "", "TOTAL:", totalAmount) + Environment.NewLine;
            invoiceContent += "=============================================================" + Environment.NewLine;
            invoiceContent += Environment.NewLine;
            invoiceContent += "Thank you for supporting sustainable living!" + Environment.NewLine;
            invoiceContent += "Every pre-loved item you purchase helps reduce environmental waste." + Environment.NewLine;
            invoiceContent += Environment.NewLine;
            invoiceContent += "=============================================================" + Environment.NewLine;

            return invoiceContent;
        }

        /// <summary>
        /// Saves invoice to a text file.
        /// </summary>
        public static void SaveInvoiceToFile(string invoiceContent, int invoiceNumber)
        {
            try
            {
                string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PreLovedFurnitureStore");
                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                string invoiceFilePath = Path.Combine(dataDirectory, $"invoice_{invoiceNumber:D6}.txt");
                File.WriteAllText(invoiceFilePath, invoiceContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving invoice: {ex.Message}");
            }
        }
    }
}
