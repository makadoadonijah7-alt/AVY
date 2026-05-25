using System;

namespace PreLovedFurnitureStore.Models
{
    /// <summary>
    /// Represents a furniture item in the store's catalogue.
    /// </summary>
    public class FurnitureItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }  // chairs, tables, sofas, storage, etc.
        public string Material { get; set; }  // wood, metal, fabric, etc.
        public decimal Price { get; set; }
        public string Condition { get; set; }  // excellent, good, fair
        public string Description { get; set; }
        public int QuantityAvailable { get; set; }

        public FurnitureItem() { }

        public FurnitureItem(int itemId, string itemName, string category, string material, 
                             decimal price, string condition, string description, int quantityAvailable)
        {
            ItemId = itemId;
            ItemName = itemName;
            Category = category;
            Material = material;
            Price = price;
            Condition = condition;
            Description = description;
            QuantityAvailable = quantityAvailable;
        }

        public override string ToString()
        {
            return $"{ItemId}|{ItemName}|{Category}|{Material}|{Price}|{Condition}|{Description}|{QuantityAvailable}";
        }

        public static FurnitureItem Parse(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 8)
            {
                return new FurnitureItem(
                    int.Parse(parts[0]),
                    parts[1],
                    parts[2],
                    parts[3],
                    decimal.Parse(parts[4]),
                    parts[5],
                    parts[6],
                    int.Parse(parts[7])
                );
            }
            return null;
        }
    }
}
