using System;
using System.Text.RegularExpressions;

namespace PreLovedFurnitureStore.Utilities
{
    /// <summary>
    /// Provides input validation methods for customer and order data.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validates that a field is not empty or whitespace.
        /// </summary>
        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Validates email format using regex.
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates phone number format (basic validation for common formats).
        /// </summary>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Remove common formatting characters
            string cleaned = Regex.Replace(phoneNumber, @"[\s\-\(\)\+]", "");
            
            // Check if it contains only digits and has reasonable length
            return Regex.IsMatch(cleaned, @"^\d{10,}$");
        }

        /// <summary>
        /// Validates that a string contains only letters and spaces.
        /// </summary>
        public static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z\s'-]{2,}$");
        }

        /// <summary>
        /// Validates that input is a valid decimal number.
        /// </summary>
        public static bool IsValidPrice(string priceStr)
        {
            return decimal.TryParse(priceStr, out decimal result) && result > 0;
        }

        /// <summary>
        /// Validates that input is a valid integer quantity.
        /// </summary>
        public static bool IsValidQuantity(string quantityStr)
        {
            return int.TryParse(quantityStr, out int result) && result > 0;
        }
    }
}
