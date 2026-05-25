using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PreLovedFurnitureStore.Utilities;

namespace PreLovedFurnitureStore
{
    /// <summary>
    /// Data Insights Form - Displays summary statistics and analytics about sales and customer behavior.
    /// </summary>
    public class DataInsightsForm : Form
    {
        private Label titleLabel;
        private Label totalSalesLabel;
        private Label totalOrdersLabel;
        private Label uniqueCustomersLabel;
        private Label averageOrderValueLabel;
        private Label mostPopularCategoryLabel;
        private Label returningCustomersLabel;
        private Panel headerPanel;
        private Panel statsPanel;
        private Button refreshButton;

        public DataInsightsForm()
        {
            InitializeComponents();
            LoadInsights();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Data Insights & Summary";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;
            headerPanel.BackColor = Color.FromArgb(33, 150, 243);
            this.Controls.Add(headerPanel);

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Analytics & Insights";
            titleLabel.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(titleLabel);

            // Stats Panel
            statsPanel = new Panel();
            statsPanel.Dock = DockStyle.Fill;
            statsPanel.BackColor = Color.White;
            statsPanel.Padding = new Padding(40);
            this.Controls.Add(statsPanel);

            // Total Sales Label
            totalSalesLabel = CreateStatLabel("Total Sales", "$0.00", 40);
            statsPanel.Controls.Add(totalSalesLabel);

            // Total Orders Label
            totalOrdersLabel = CreateStatLabel("Total Orders", "0", 140);
            statsPanel.Controls.Add(totalOrdersLabel);

            // Unique Customers Label
            uniqueCustomersLabel = CreateStatLabel("Unique Customers", "0", 240);
            statsPanel.Controls.Add(uniqueCustomersLabel);

            // Average Order Value Label
            averageOrderValueLabel = CreateStatLabel("Average Order Value", "$0.00", 340);
            statsPanel.Controls.Add(averageOrderValueLabel);

            // Most Popular Category Label
            mostPopularCategoryLabel = CreateStatLabel("Most Popular Category", "N/A", 440);
            statsPanel.Controls.Add(mostPopularCategoryLabel);

            // Returning Customers Label
            returningCustomersLabel = CreateStatLabel("Returning Customers", "0", 540);
            statsPanel.Controls.Add(returningCustomersLabel);

            // Refresh Button
            refreshButton = new Button();
            refreshButton.Text = "Refresh Data";
            refreshButton.Dock = DockStyle.Bottom;
            refreshButton.Height = 50;
            refreshButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            refreshButton.BackColor = Color.FromArgb(33, 150, 243);
            refreshButton.ForeColor = Color.White;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Click += (s, e) => LoadInsights();
            this.Controls.Add(refreshButton);
        }

        private Label CreateStatLabel(string title, string value, int yPosition)
        {
            Panel statBox = new Panel();
            statBox.Location = new Point(40, yPosition);
            statBox.Size = new Size(720, 80);
            statBox.BorderStyle = BorderStyle.FixedSingle;
            statBox.BackColor = Color.FromArgb(240, 240, 240);
            statsPanel.Controls.Add(statBox);

            Label titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            titleLabel.Location = new Point(20, 10);
            titleLabel.Size = new Size(400, 25);
            titleLabel.ForeColor = Color.FromArgb(60, 60, 60);
            statBox.Controls.Add(titleLabel);

            Label valueLabel = new Label();
            valueLabel.Text = value;
            valueLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            valueLabel.Location = new Point(20, 40);
            valueLabel.Size = new Size(400, 30);
            valueLabel.ForeColor = Color.FromArgb(33, 150, 243);
            statBox.Controls.Add(valueLabel);

            return valueLabel;
        }

        private void LoadInsights()
        {
            try
            {
                List<string> orders = FileHandler.GetAllOrders();

                if (orders.Count == 0)
                {
                    totalSalesLabel.Text = "$0.00";
                    totalOrdersLabel.Text = "0";
                    uniqueCustomersLabel.Text = "0";
                    averageOrderValueLabel.Text = "$0.00";
                    mostPopularCategoryLabel.Text = "N/A";
                    returningCustomersLabel.Text = "0";
                    return;
                }

                // Calculate analytics
                decimal totalSales = DataAnalytics.GetTotalSales(orders);
                int totalOrders = DataAnalytics.GetTotalOrders(orders);
                int uniqueCustomers = DataAnalytics.GetUniqueCustomerCount(orders);
                decimal avgOrderValue = DataAnalytics.GetAverageOrderValue(orders);
                string mostPopularCategory = DataAnalytics.GetMostPopularCategory(orders);
                int returningCustomers = DataAnalytics.GetReturningCustomerCount(orders);

                // Update labels
                totalSalesLabel.Text = $"${totalSales:F2}";
                totalOrdersLabel.Text = totalOrders.ToString();
                uniqueCustomersLabel.Text = uniqueCustomers.ToString();
                averageOrderValueLabel.Text = $"${avgOrderValue:F2}";
                mostPopularCategoryLabel.Text = mostPopularCategory;
                returningCustomersLabel.Text = returningCustomers.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading insights: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
