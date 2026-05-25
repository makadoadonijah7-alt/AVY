using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PreLovedFurnitureStore.Models;
using PreLovedFurnitureStore.Utilities;

namespace PreLovedFurnitureStore
{
    /// <summary>
    /// Customer Details Form - Captures customer information and generates invoice.
    /// </summary>
    public class CustomerDetailsForm : Form
    {
        private Label titleLabel;
        private Label nameLabel;
        private TextBox nameTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label phoneLabel;
        private TextBox phoneTextBox;
        private Label addressLabel;
        private TextBox addressTextBox;
        private Label deliveryLabel;
        private ComboBox deliveryComboBox;
        private Button submitButton;
        private Button cancelButton;
        private List<OrderItem> selectedItems;
        private int invoiceNumber;
        private decimal totalAmount;
        private Panel headerPanel;

        public CustomerDetailsForm(List<OrderItem> items, int invoiceNum)
        {
            selectedItems = items;
            invoiceNumber = invoiceNum;
            CalculateTotal();
            InitializeComponents();
        }

        private void CalculateTotal()
        {
            totalAmount = 0;
            foreach (var item in selectedItems)
            {
                totalAmount += item.GetSubtotal();
            }
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Customer Details & Invoice";
            this.Size = new Size(600, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(156, 39, 176);  // Purple
            this.Controls.Add(headerPanel);

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Complete Your Purchase";
            titleLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(titleLabel);

            // Name Label & TextBox
            nameLabel = new Label();
            nameLabel.Text = "Full Name:";
            nameLabel.Location = new Point(30, 80);
            nameLabel.Size = new Size(150, 25);
            nameLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(nameLabel);

            nameTextBox = new TextBox();
            nameTextBox.Location = new Point(30, 110);
            nameTextBox.Size = new Size(520, 35);
            nameTextBox.Font = new Font("Segoe UI", 11);
            nameTextBox.Multiline = false;
            this.Controls.Add(nameTextBox);

            // Email Label & TextBox
            emailLabel = new Label();
            emailLabel.Text = "Email Address:";
            emailLabel.Location = new Point(30, 155);
            emailLabel.Size = new Size(150, 25);
            emailLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(emailLabel);

            emailTextBox = new TextBox();
            emailTextBox.Location = new Point(30, 185);
            emailTextBox.Size = new Size(520, 35);
            emailTextBox.Font = new Font("Segoe UI", 11);
            emailTextBox.Multiline = false;
            this.Controls.Add(emailTextBox);

            // Phone Label & TextBox
            phoneLabel = new Label();
            phoneLabel.Text = "Phone Number:";
            phoneLabel.Location = new Point(30, 230);
            phoneLabel.Size = new Size(150, 25);
            phoneLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(phoneLabel);

            phoneTextBox = new TextBox();
            phoneTextBox.Location = new Point(30, 260);
            phoneTextBox.Size = new Size(520, 35);
            phoneTextBox.Font = new Font("Segoe UI", 11);
            phoneTextBox.Multiline = false;
            this.Controls.Add(phoneTextBox);

            // Address Label & TextBox
            addressLabel = new Label();
            addressLabel.Text = "Delivery Address:";
            addressLabel.Location = new Point(30, 305);
            addressLabel.Size = new Size(150, 25);
            addressLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(addressLabel);

            addressTextBox = new TextBox();
            addressTextBox.Location = new Point(30, 335);
            addressTextBox.Size = new Size(520, 80);
            addressTextBox.Font = new Font("Segoe UI", 11);
            addressTextBox.Multiline = true;
            this.Controls.Add(addressTextBox);

            // Delivery Method Label & ComboBox
            deliveryLabel = new Label();
            deliveryLabel.Text = "Delivery Method:";
            deliveryLabel.Location = new Point(30, 430);
            deliveryLabel.Size = new Size(150, 25);
            deliveryLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(deliveryLabel);

            deliveryComboBox = new ComboBox();
            deliveryComboBox.Location = new Point(30, 460);
            deliveryComboBox.Size = new Size(520, 35);
            deliveryComboBox.Font = new Font("Segoe UI", 11);
            deliveryComboBox.Items.AddRange(new string[] { "Delivery", "Collection" });
            deliveryComboBox.SelectedIndex = 0;
            deliveryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(deliveryComboBox);

            // Submit Button
            submitButton = new Button();
            submitButton.Text = "Complete Purchase";
            submitButton.Location = new Point(30, 520);
            submitButton.Size = new Size(250, 50);
            submitButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            submitButton.BackColor = Color.FromArgb(76, 175, 80);
            submitButton.ForeColor = Color.White;
            submitButton.FlatStyle = FlatStyle.Flat;
            submitButton.Click += SubmitButton_Click;
            this.Controls.Add(submitButton);

            // Cancel Button
            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(300, 520);
            cancelButton.Size = new Size(250, 50);
            cancelButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            cancelButton.BackColor = Color.FromArgb(244, 67, 54);
            cancelButton.ForeColor = Color.White;
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Click += (s, e) => this.Close();
            this.Controls.Add(cancelButton);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                // Create customer and order
                Customer customer = new Customer(
                    invoiceNumber,
                    nameTextBox.Text.Trim(),
                    emailTextBox.Text.Trim(),
                    phoneTextBox.Text.Trim(),
                    addressTextBox.Text.Trim(),
                    deliveryComboBox.SelectedItem.ToString()
                );

                Order order = new Order(invoiceNumber, invoiceNumber, totalAmount, deliveryComboBox.SelectedItem.ToString());

                // Save to file
                FileHandler.SaveOrder(order, customer, selectedItems);
                FileHandler.SaveCustomer(customer);

                // Generate invoice
                string invoiceContent = InvoiceGenerator.GenerateInvoice(invoiceNumber, customer, selectedItems, totalAmount, deliveryComboBox.SelectedItem.ToString());
                InvoiceGenerator.SaveInvoiceToFile(invoiceContent, invoiceNumber);

                // Show invoice
                MessageBox.Show(invoiceContent, "Purchase Successful - Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error completing purchase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            // Validate name
            if (!Validator.IsNotEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Please enter your full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }

            if (!Validator.IsValidName(nameTextBox.Text))
            {
                MessageBox.Show("Name must contain only letters and spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }

            // Validate email
            if (!Validator.IsNotEmpty(emailTextBox.Text))
            {
                MessageBox.Show("Please enter your email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailTextBox.Focus();
                return false;
            }

            if (!Validator.IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailTextBox.Focus();
                return false;
            }

            // Validate phone
            if (!Validator.IsNotEmpty(phoneTextBox.Text))
            {
                MessageBox.Show("Please enter your phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phoneTextBox.Focus();
                return false;
            }

            if (!Validator.IsValidPhoneNumber(phoneTextBox.Text))
            {
                MessageBox.Show("Please enter a valid phone number (at least 10 digits).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phoneTextBox.Focus();
                return false;
            }

            // Validate address
            if (!Validator.IsNotEmpty(addressTextBox.Text))
            {
                MessageBox.Show("Please enter your delivery address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                addressTextBox.Focus();
                return false;
            }

            return true;
        }
    }
}
