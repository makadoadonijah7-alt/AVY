using System;
using System.Drawing;
using System.Windows.Forms;

namespace PreLovedFurnitureStore
{
    /// <summary>
    /// About Form - Provides information about the store's mission, values, and commitment to sustainability.
    /// </summary>
    public class AboutForm : Form
    {
        private Label titleLabel;
        private RichTextBox contentTextBox;
        private Button closeButton;
        private Panel headerPanel;

        public AboutForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "About Pre-Loved Furniture Store";
            this.Size = new Size(700, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;
            headerPanel.BackColor = Color.FromArgb(255, 152, 0);  // Orange
            this.Controls.Add(headerPanel);

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "About Us";
            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(titleLabel);

            // Content Text Box
            contentTextBox = new RichTextBox();
            contentTextBox.Dock = DockStyle.Fill;
            contentTextBox.ReadOnly = true;
            contentTextBox.BackColor = Color.White;
            contentTextBox.Font = new Font("Segoe UI", 11);
            contentTextBox.Text = GetAboutContent();
            this.Controls.Add(contentTextBox);

            // Close Button
            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Dock = DockStyle.Bottom;
            closeButton.Height = 50;
            closeButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            closeButton.BackColor = Color.FromArgb(255, 152, 0);
            closeButton.ForeColor = Color.White;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Click += (s, e) => this.Close();
            this.Controls.Add(closeButton);
        }

        private string GetAboutContent()
        {
            return @"OUR MISSION

At Pre-Loved Furniture Store, our mission is to give quality furniture a second life while promoting sustainable living and reducing environmental impact. We believe that beautiful, well-made furniture shouldn't end up in landfills just because it's no longer needed by its original owner.


OUR VALUES

- Sustainability: Every item purchased reduces demand for new production, thereby conserving natural resources and reducing carbon footprints.

- Reuse & Circular Economy: We believe in the circular economy where products are valued not just for their newness, but for their quality and longevity.

- Environmental Responsibility: By choosing pre-loved furniture, you're making a conscious choice to protect our planet for future generations.

- Quality & Affordability: We carefully curate our collection to ensure every item meets our quality standards while remaining affordable.


BENEFITS OF BUYING PRE-LOVED FURNITURE

- Environmental Impact: Reduce waste and lower carbon emissions by extending the life of quality furniture

- Cost Savings: Save up to 50-70% compared to new furniture without compromising on quality

- Unique Pieces: Find one-of-a-kind items and vintage treasures that won't be found in every home

- Durability: Pre-loved furniture often features superior craftsmanship compared to modern mass-produced alternatives

- Sustainability Contribution: Support circular economy practices and responsible consumption


OUR COMMITMENT

We are committed to:
- Providing transparent information about each item's condition
- Rigorous quality checking and refurbishment when necessary
- Fair pricing that reflects quality and condition
- Excellent customer service and support
- Promoting awareness about sustainable consumption
- Continuously improving our environmental practices

Join us in making a difference!
Every purchase is a vote for a sustainable future.";
        }
    }
}
