using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PreLovedFurnitureStore.Utilities;

namespace PreLovedFurnitureStore
{
    /// <summary>
    /// Purchase History Form - Displays previous customer purchases in a grid layout.
    /// </summary>
    public class PurchaseHistoryForm : Form
    {
        private DataGridView historyGridView;
        private Button refreshButton;
        private Button viewDetailsButton;
        private Label titleLabel;
        private Panel headerPanel;
        private Label totalOrdersLabel;
        private Label totalSalesLabel;

        public PurchaseHistoryForm()
        {
            InitializeComponents();
            LoadPurchaseHistory();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Purchase History";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;
            headerPanel.BackColor = Color.FromArgb(156, 39, 176);
            this.Controls.Add(headerPanel);

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Order History";
            titleLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(20, 15);
            titleLabel.Size = new Size(400, 40);
            headerPanel.Controls.Add(titleLabel);

            // Total Orders Label
            totalOrdersLabel = new Label();
            totalOrdersLabel.Font = new Font("Segoe UI", 10);
            totalOrdersLabel.ForeColor = Color.White;
            totalOrdersLabel.Location = new Point(20, 55);
            totalOrdersLabel.Size = new Size(300, 20);
            headerPanel.Controls.Add(totalOrdersLabel);

            // Total Sales Label
            totalSalesLabel = new Label();
            totalSalesLabel.Font = new Font("Segoe UI", 10);
            totalSalesLabel.ForeColor = Color.White;
            totalSalesLabel.Location = new Point(320, 55);
            totalSalesLabel.Size = new Size(300, 20);
            headerPanel.Controls.Add(totalSalesLabel);

            // History GridView
            historyGridView = new DataGridView();
            historyGridView.Location = new Point(20, 100);
            historyGridView.Size = new Size(940, 500);
            historyGridView.AllowUserToAddRows = false;
            historyGridView.ReadOnly = true;
            historyGridView.BackgroundColor = Color.White;
            historyGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80);
            historyGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            historyGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            historyGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // Add columns
            historyGridView.Columns.Add("OrderID", "Order #");
            historyGridView.Columns.Add("CustomerName", "Customer Name");
            historyGridView.Columns.Add("Email", "Email");
            historyGridView.Columns.Add("Phone", "Phone");
            historyGridView.Columns.Add("DeliveryMethod", "Delivery");
            historyGridView.Columns.Add("TotalAmount", "Total");
            historyGridView.Columns.Add("OrderDate", "Order Date");
            
            historyGridView.Columns["OrderID"].Width = 70;
            historyGridView.Columns["CustomerName"].Width = 150;
            historyGridView.Columns["Email"].Width = 180;
            historyGridView.Columns["Phone"].Width = 120;
            historyGridView.Columns["DeliveryMethod"].Width = 100;
            historyGridView.Columns["TotalAmount"].Width = 100;
            historyGridView.Columns["OrderDate"].Width = 120;
            
            this.Controls.Add(historyGridView);

            // View Details Button
            viewDetailsButton = new Button();
            viewDetailsButton.Text = "View Details";
            viewDetailsButton.Location = new Point(380, 620);
            viewDetailsButton.Size = new Size(200, 40);
            viewDetailsButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            viewDetailsButton.BackColor = Color.FromArgb(33, 150, 243);
            viewDetailsButton.ForeColor = Color.White;
            viewDetailsButton.FlatStyle = FlatStyle.Flat;
            viewDetailsButton.Click += ViewDetailsButton_Click;
            this.Controls.Add(viewDetailsButton);

            // Refresh Button
            refreshButton = new Button();
            refreshButton.Text = "Refresh";
            refreshButton.Location = new Point(600, 620);
            refreshButton.Size = new Size(200, 40);
            refreshButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            refreshButton.BackColor = Color.FromArgb(76, 175, 80);
            refreshButton.ForeColor = Color.White;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Click += (s, e) => LoadPurchaseHistory();
            this.Controls.Add(refreshButton);
        }

        private void LoadPurchaseHistory()
        {
            try
            {
                historyGridView.Rows.Clear();
                List<string> orders = FileHandler.GetAllOrders();

                if (orders.Count == 0)
                {
                    MessageBox.Show("No purchase history available.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    totalOrdersLabel.Text = "Total Orders: 0";
                    totalSalesLabel.Text = "Total Sales: $0.00";
                    return;
                }

                foreach (var order in orders)
                {
                    string[] parts = order.Split('|');
                    if (parts.Length >= 8)
                    {
                        historyGridView.Rows.Add(
                            parts[0],  // OrderID
                            parts[1],  // CustomerName
                            parts[2],  // Email
                            parts[3],  // Phone
                            parts[5],  // DeliveryMethod
                            $"${decimal.Parse(parts[6]):F2}",  // TotalAmount
                            parts[7]   // OrderDate
                        );
                    }
                }

                // Update summary labels
                totalOrdersLabel.Text = $"Total Orders: {orders.Count}";
                decimal totalSales = DataAnalytics.GetTotalSales(orders);
                totalSalesLabel.Text = $"Total Sales: ${totalSales:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewDetailsButton_Click(object sender, EventArgs e)
        {
            if (historyGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to view details.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orderId = historyGridView.SelectedRows[0].Cells["OrderID"].Value.ToString();
            List<string> items = FileHandler.GetOrderItems(int.Parse(orderId));

            string details = $"Order Details for Order #{orderId}\n\n";
            foreach (var item in items)
            {
                details += item + "\n";
            }

            MessageBox.Show(details, "Order Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
