using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PreLovedFurnitureStore.Models;
using PreLovedFurnitureStore.Utilities;

namespace PreLovedFurnitureStore
{
    /// <summary>
    /// Furniture Catalogue Form - Allows customers to browse furniture items and make purchases.
    /// </summary>
    public class FurnitureCatalogueForm : Form
    {
        private DataGridView furnitureGridView;
        private Label selectedItemsLabel;
        private ListBox selectedItemsListBox;
        private Label totalPriceLabel;
        private Button checkoutButton;
        private Button removeItemButton;
        private ComboBox categoryFilterCombo;
        private Label categoryFilterLabel;
        private Button clearSelectionButton;
        private List<OrderItem> selectedItems;
        private List<FurnitureItem> catalogueItems;
        private int invoiceCounter = 1;

        public FurnitureCatalogueForm()
        {
            selectedItems = new List<OrderItem>();
            catalogueItems = new List<FurnitureItem>();
            InitializeComponents();
            LoadSampleData();
            RefreshGrid();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Furniture Catalogue";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Category Filter Label
            categoryFilterLabel = new Label();
            categoryFilterLabel.Text = "Filter by Category:";
            categoryFilterLabel.Location = new Point(20, 20);
            categoryFilterLabel.Size = new Size(120, 30);
            categoryFilterLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(categoryFilterLabel);

            // Category Filter ComboBox
            categoryFilterCombo = new ComboBox();
            categoryFilterCombo.Location = new Point(150, 20);
            categoryFilterCombo.Size = new Size(150, 30);
            categoryFilterCombo.Items.AddRange(new string[] { "All", "Chairs", "Tables", "Sofas", "Storage" });
            categoryFilterCombo.SelectedIndex = 0;
            categoryFilterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryFilterCombo.SelectedIndexChanged += CategoryFilterCombo_SelectedIndexChanged;
            this.Controls.Add(categoryFilterCombo);

            // Furniture GridView
            furnitureGridView = new DataGridView();
            furnitureGridView.Location = new Point(20, 60);
            furnitureGridView.Size = new Size(600, 600);
            furnitureGridView.AllowUserToAddRows = false;
            furnitureGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            furnitureGridView.BackgroundColor = Color.White;
            furnitureGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80);
            furnitureGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            furnitureGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            
            // Add columns
            furnitureGridView.Columns.Add("ItemID", "ID");
            furnitureGridView.Columns.Add("ItemName", "Item Name");
            furnitureGridView.Columns.Add("Category", "Category");
            furnitureGridView.Columns.Add("Material", "Material");
            furnitureGridView.Columns.Add("Price", "Price");
            furnitureGridView.Columns.Add("Condition", "Condition");
            
            furnitureGridView.Columns["ItemID"].Width = 40;
            furnitureGridView.Columns["ItemName"].Width = 120;
            furnitureGridView.Columns["Category"].Width = 80;
            furnitureGridView.Columns["Material"].Width = 80;
            furnitureGridView.Columns["Price"].Width = 70;
            furnitureGridView.Columns["Condition"].Width = 80;
            
            this.Controls.Add(furnitureGridView);

            // Add Button (to add selected item to cart)
            Button addButton = new Button();
            addButton.Text = "Add to Cart";
            addButton.Location = new Point(630, 60);
            addButton.Size = new Size(450, 40);
            addButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            addButton.BackColor = Color.FromArgb(76, 175, 80);
            addButton.ForeColor = Color.White;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            // Selected Items Label
            selectedItemsLabel = new Label();
            selectedItemsLabel.Text = "Selected Items:";
            selectedItemsLabel.Location = new Point(630, 110);
            selectedItemsLabel.Size = new Size(450, 25);
            selectedItemsLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.Controls.Add(selectedItemsLabel);

            // Selected Items ListBox
            selectedItemsListBox = new ListBox();
            selectedItemsListBox.Location = new Point(630, 140);
            selectedItemsListBox.Size = new Size(450, 300);
            selectedItemsListBox.BackColor = Color.FromArgb(245, 245, 245);
            this.Controls.Add(selectedItemsListBox);

            // Remove Item Button
            removeItemButton = new Button();
            removeItemButton.Text = "Remove Selected";
            removeItemButton.Location = new Point(630, 450);
            removeItemButton.Size = new Size(220, 35);
            removeItemButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            removeItemButton.BackColor = Color.FromArgb(244, 67, 54);  // Red
            removeItemButton.ForeColor = Color.White;
            removeItemButton.FlatStyle = FlatStyle.Flat;
            removeItemButton.Click += RemoveItemButton_Click;
            this.Controls.Add(removeItemButton);

            // Clear Selection Button
            clearSelectionButton = new Button();
            clearSelectionButton.Text = "Clear All";
            clearSelectionButton.Location = new Point(860, 450);
            clearSelectionButton.Size = new Size(220, 35);
            clearSelectionButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            clearSelectionButton.BackColor = Color.FromArgb(158, 158, 158);
            clearSelectionButton.ForeColor = Color.White;
            clearSelectionButton.FlatStyle = FlatStyle.Flat;
            clearSelectionButton.Click += ClearSelectionButton_Click;
            this.Controls.Add(clearSelectionButton);

            // Total Price Label
            totalPriceLabel = new Label();
            totalPriceLabel.Location = new Point(630, 500);
            totalPriceLabel.Size = new Size(450, 40);
            totalPriceLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            totalPriceLabel.TextAlign = ContentAlignment.MiddleRight;
            totalPriceLabel.ForeColor = Color.FromArgb(76, 175, 80);
            this.Controls.Add(totalPriceLabel);

            // Checkout Button
            checkoutButton = new Button();
            checkoutButton.Text = "Proceed to Checkout";
            checkoutButton.Location = new Point(630, 550);
            checkoutButton.Size = new Size(450, 50);
            checkoutButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            checkoutButton.BackColor = Color.FromArgb(156, 39, 176);  // Purple
            checkoutButton.ForeColor = Color.White;
            checkoutButton.FlatStyle = FlatStyle.Flat;
            checkoutButton.Click += CheckoutButton_Click;
            this.Controls.Add(checkoutButton);
        }

        private void LoadSampleData()
        {
            // Add sample furniture items
            catalogueItems.Add(new FurnitureItem(1, "Wooden Dining Chair", "Chairs", "Wood", 45.99m, "Excellent", "Classic wooden chair in perfect condition", 3));
            catalogueItems.Add(new FurnitureItem(2, "Office Chair", "Chairs", "Metal & Fabric", 89.99m, "Good", "Ergonomic office chair with wheels", 2));
            catalogueItems.Add(new FurnitureItem(3, "Round Coffee Table", "Tables", "Wood", 125.00m, "Excellent", "Solid oak coffee table with smooth finish", 1));
            catalogueItems.Add(new FurnitureItem(4, "Glass Top Dining Table", "Tables", "Metal & Glass", 249.99m, "Fair", "Elegant dining table for 6 persons", 1));
            catalogueItems.Add(new FurnitureItem(5, "Leather Sofa", "Sofas", "Leather", 599.99m, "Good", "3-seater brown leather sofa", 1));
            catalogueItems.Add(new FurnitureItem(6, "Fabric Sofa", "Sofas", "Fabric", 399.99m, "Excellent", "Grey fabric sofa with modern design", 2));
            catalogueItems.Add(new FurnitureItem(7, "Bookshelf", "Storage", "Wood", 179.99m, "Good", "5-shelf wooden bookcase", 2));
            catalogueItems.Add(new FurnitureItem(8, "Filing Cabinet", "Storage", "Metal", 99.99m, "Excellent", "4-drawer metal filing cabinet", 3));
            catalogueItems.Add(new FurnitureItem(9, "Armchair", "Chairs", "Fabric", 199.99m, "Excellent", "Comfortable armchair with ottoman", 1));
            catalogueItems.Add(new FurnitureItem(10, "Side Table", "Tables", "Wood", 59.99m, "Good", "Small wooden side table", 4));
        }

        private void RefreshGrid()
        {
            furnitureGridView.Rows.Clear();
            string selectedCategory = categoryFilterCombo.SelectedItem.ToString();

            foreach (var item in catalogueItems)
            {
                if (selectedCategory == "All" || item.Category == selectedCategory)
                {
                    furnitureGridView.Rows.Add(item.ItemId, item.ItemName, item.Category, 
                                              item.Material, $"${item.Price:F2}", item.Condition);
                }
            }
        }

        private void CategoryFilterCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (furnitureGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to add to cart.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int itemId = int.Parse(furnitureGridView.SelectedRows[0].Cells["ItemID"].Value.ToString());
            FurnitureItem selectedFurniture = catalogueItems.Find(x => x.ItemId == itemId);

            if (selectedFurniture != null)
            {
                OrderItem orderItem = new OrderItem(selectedFurniture.ItemId, selectedFurniture.ItemName,
                                                   selectedFurniture.Price, 1, selectedFurniture.Category,
                                                   selectedFurniture.Material);
                selectedItems.Add(orderItem);
                UpdateSelectedItemsDisplay();
                MessageBox.Show($"{selectedFurniture.ItemName} added to cart!", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoveItemButton_Click(object sender, EventArgs e)
        {
            if (selectedItemsListBox.SelectedIndex >= 0)
            {
                selectedItems.RemoveAt(selectedItemsListBox.SelectedIndex);
                UpdateSelectedItemsDisplay();
            }
        }

        private void ClearSelectionButton_Click(object sender, EventArgs e)
        {
            selectedItems.Clear();
            UpdateSelectedItemsDisplay();
        }

        private void UpdateSelectedItemsDisplay()
        {
            selectedItemsListBox.Items.Clear();
            decimal total = 0;

            foreach (var item in selectedItems)
            {
                string displayText = $"{item.ItemName} - ${item.ItemPrice:F2}";
                selectedItemsListBox.Items.Add(displayText);
                total += item.GetSubtotal();
            }

            totalPriceLabel.Text = $"Total: ${total:F2}";
        }

        private void CheckoutButton_Click(object sender, EventArgs e)
        {
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one item.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerDetailsForm customerForm = new CustomerDetailsForm(selectedItems, invoiceCounter++);
            customerForm.Show();
            this.Close();
        }
    }
}
