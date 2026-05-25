using System;

namespace PreLovedFurnitureStore.Models
{
    /// <summary>
    /// Represents a customer of the pre-loved furniture store.
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DeliveryPreference { get; set; }  // delivery or collection
        public DateTime RegisteredDate { get; set; }

        public Customer() { }

        public Customer(int customerId, string fullName, string email, string phoneNumber, 
                       string address, string deliveryPreference)
        {
            CustomerId = customerId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            DeliveryPreference = deliveryPreference;
            RegisteredDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{CustomerId}|{FullName}|{Email}|{PhoneNumber}|{Address}|{DeliveryPreference}|{RegisteredDate}";
        }

        public static Customer Parse(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length >= 6)
            {
                return new Customer(
                    int.Parse(parts[0]),
                    parts[1],
                    parts[2],
                    parts[3],
                    parts[4],
                    parts[5]
                );
            }
            return null;
        }
    }
}
