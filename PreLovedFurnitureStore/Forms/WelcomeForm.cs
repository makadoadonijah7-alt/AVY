using System;
using System.Drawing;
using System.Windows.Forms;

namespace PreLovedFurnitureStore
{
    /// <summary>
    /// Welcome Form - First form displayed to users.
    /// Introduces the business and provides navigation to other forms.
    /// </summary>
    public class WelcomeForm : Form
    {
        private Label titleLabel;
        private Label subtitleLabel;
        private Button browseCatalogueButton;
        private Button purchaseHistoryButton;
        private Button dataInsightsButton;
        private Button aboutButton;
        private Label welcomeMessageLabel;
        private Panel headerPanel;
        private Panel contentPanel;

        public WelcomeForm()
        {
            InitializeComponents();
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Pre-Loved Furniture Store - Welcome";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 150;
            headerPanel.BackColor = Color.FromArgb(76, 175, 80);  // Green - sustainability theme
            this.Controls.Add(headerPanel);

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Pre-Loved Furniture Store";
            titleLabel.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.TextAlign = ContentAlignment.TopCenter;
            titleLabel.Padding = new Padding(0, 20, 0, 0);
            headerPanel.Controls.Add(titleLabel);

            // Subtitle Label
            subtitleLabel = new Label();
            subtitleLabel.Text = "Giving Quality Furniture a Second Life";
            subtitleLabel.Font = new Font("Segoe UI", 14, FontStyle.Italic);
            subtitleLabel.ForeColor = Color.White;
            subtitleLabel.Dock = DockStyle.Fill;
            subtitleLabel.TextAlign = ContentAlignment.TopCenter;
            headerPanel.Controls.Add(subtitleLabel);

            // Content Panel
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // Welcome Message
            welcomeMessageLabel = new Label();
            welcomeMessageLabel.Text = "Welcome to our sustainable furniture marketplace!\n\n" +
                                      "We believe in giving quality furniture a second life while promoting\n" +
                                      "sustainable living and reducing environmental impact.\n\n" +
                                      "Explore our collection of carefully selected pre-loved items.";
            welcomeMessageLabel.Font = new Font("Segoe UI", 12);
            welcomeMessageLabel.ForeColor = Color.FromArgb(60, 60, 60);
            welcomeMessageLabel.Location = new Point(50, 30);
            welcomeMessageLabel.Size = new Size(700, 120);
            welcomeMessageLabel.TextAlign = ContentAlignment.TopCenter;
            contentPanel.Controls.Add(welcomeMessageLabel);

            // Browse Catalogue Button
            browseCatalogueButton = new Button();
            browseCatalogueButton.Text = "Browse Catalogue";
            browseCatalogueButton.Location = new Point(50, 170);
            browseCatalogueButton.Size = new Size(700, 50);
            browseCatalogueButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            browseCatalogueButton.BackColor = Color.FromArgb(76, 175, 80);
            browseCatalogueButton.ForeColor = Color.White;
            browseCatalogueButton.FlatStyle = FlatStyle.Flat;
            browseCatalogueButton.Click += BrowseCatalogueButton_Click;
            contentPanel.Controls.Add(browseCatalogueButton);

            // Purchase History Button
            purchaseHistoryButton = new Button();
            purchaseHistoryButton.Text = "Purchase History";
            purchaseHistoryButton.Location = new Point(50, 230);
            purchaseHistoryButton.Size = new Size(700, 50);
            purchaseHistoryButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            purchaseHistoryButton.BackColor = Color.FromArgb(156, 39, 176);  // Purple
            purchaseHistoryButton.ForeColor = Color.White;
            purchaseHistoryButton.FlatStyle = FlatStyle.Flat;
            purchaseHistoryButton.Click += PurchaseHistoryButton_Click;
            contentPanel.Controls.Add(purchaseHistoryButton);

            // Data Insights Button
            dataInsightsButton = new Button();
            dataInsightsButton.Text = "Data Insights";
            dataInsightsButton.Location = new Point(50, 290);
            dataInsightsButton.Size = new Size(700, 50);
            dataInsightsButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataInsightsButton.BackColor = Color.FromArgb(33, 150, 243);  // Blue
            dataInsightsButton.ForeColor = Color.White;
            dataInsightsButton.FlatStyle = FlatStyle.Flat;
            dataInsightsButton.Click += DataInsightsButton_Click;
            contentPanel.Controls.Add(dataInsightsButton);

            // About Button
            aboutButton = new Button();
            aboutButton.Text = "About Us";
            aboutButton.Location = new Point(50, 350);
            aboutButton.Size = new Size(700, 50);
            aboutButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            aboutButton.BackColor = Color.FromArgb(255, 152, 0);  // Orange
            aboutButton.ForeColor = Color.White;
            aboutButton.FlatStyle = FlatStyle.Flat;
            aboutButton.Click += AboutButton_Click;
            contentPanel.Controls.Add(aboutButton);
        }

        private void BrowseCatalogueButton_Click(object sender, EventArgs e)
        {
            FurnitureCatalogueForm catalogueForm = new FurnitureCatalogueForm();
            catalogueForm.Show();
        }

        private void PurchaseHistoryButton_Click(object sender, EventArgs e)
        {
            PurchaseHistoryForm historyForm = new PurchaseHistoryForm();
            historyForm.Show();
        }

        private void DataInsightsButton_Click(object sender, EventArgs e)
        {
            DataInsightsForm insightsForm = new DataInsightsForm();
            insightsForm.Show();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }
    }
}
