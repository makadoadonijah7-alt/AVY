# Pre-Loved Furniture Store - C# Windows Forms Application

## Project Overview
A comprehensive C# Windows Forms application for an online pre-loved furniture store. The application enables customers to browse furniture items, make purchases, view order history, and provides store analytics and insights.

## ✅ Requirements Fulfilled

### Forms Implemented:
1. **Welcome Form** - Introduction & navigation hub
2. **About Form** - Store mission, values, and sustainability info
3. **Furniture Catalogue Form** - Browse & select items with filters
4. **Customer Details Form** - Capture customer info & generate invoices
5. **Purchase History Form** - View previous orders in grid layout
6. **Data Insights Form** - Analytics & sales summaries

### Key Features:
✓ Category filtering (Chairs, Tables, Sofas, Storage)
✓ Shopping cart functionality
✓ Input validation (name, email, phone, address)
✓ Professional invoice generation
✓ Text file persistence for orders & customers
✓ Analytics (total sales, popular items, returning customers)
✓ Modern UI with eco-friendly green theme
✓ 10 pre-loaded sample furniture items

## 🚀 How to Run

### Requirements:
- Windows OS (XP or later)
- .NET Framework 4.7.2 or higher
- Visual Studio 2015+ (to build from source)

### Option 1: Run from Visual Studio
1. Open `PreLovedFurnitureStore.sln` in Visual Studio
2. Press `F5` or click "Run"
3. The Welcome Form will appear

### Option 2: Run Compiled EXE
1. Navigate to `bin/Debug/` or `bin/Release/`
2. Double-click `PreLovedFurnitureStore.exe`
3. The application will launch

## 📁 Project Structure

```
PreLovedFurnitureStore/
├── Program.cs                      # Entry point
├── App.config                      # Configuration
├── PreLovedFurnitureStore.csproj  # Project file
│
├── Models/
│   ├── FurnitureItem.cs           # Furniture model
│   ├── Customer.cs                 # Customer model
│   └── Order.cs                    # Order & OrderItem models
│
├── Utilities/
│   ├── FileHandler.cs              # File I/O operations
│   ├── Validator.cs                # Input validation
│   ├── InvoiceGenerator.cs         # Invoice creation
│   └── DataAnalytics.cs            # Sales analytics
│
├── Forms/
│   ├── WelcomeForm.cs
│   ├── AboutForm.cs
│   ├── FurnitureCatalogueForm.cs
│   ├── CustomerDetailsForm.cs
│   ├── PurchaseHistoryForm.cs
│   └── DataInsightsForm.cs
│
└── README.md                        # This file
```

## 💾 Data Storage

All data is stored in text files located at:
```
C:\Users\[YourUsername]\AppData\Roaming\PreLovedFurnitureStore\
```

Files created:
- `orders.txt` - Order records
- `customers.txt` - Customer information
- `order_[OrderID]_items.txt` - Items in each order
- `invoice_[InvoiceNumber].txt` - Generated invoices

## 🎨 Color Scheme

- **Primary Green** (#4CAF50) - Sustainability focus
- **Secondary Purple** (#9C27F0) - Customer details
- **Accent Blue** (#2196F3) - Insights & analytics
- **Warning Orange** (#FF9800) - About section
- **Error Red** (#F44336) - Delete/Cancel actions
- **Neutral Gray** (#9E9E9E) - Secondary actions

## 📊 Sample Furniture Data

| Item | Category | Material | Price |
|------|----------|----------|-------|
| Wooden Dining Chair | Chairs | Wood | $45.99 |
| Office Chair | Chairs | Metal & Fabric | $89.99 |
| Round Coffee Table | Tables | Wood | $125.00 |
| Glass Top Dining Table | Tables | Metal & Glass | $249.99 |
| Leather Sofa | Sofas | Leather | $599.99 |
| Fabric Sofa | Sofas | Fabric | $399.99 |
| Bookshelf | Storage | Wood | $179.99 |
| Filing Cabinet | Storage | Metal | $99.99 |
| Armchair | Chairs | Fabric | $199.99 |
| Side Table | Tables | Wood | $59.99 |

## ✨ Key Functionalities

### Browse & Shop
- View furniture with category filters
- Add items to shopping cart
- Remove items or clear entire cart
- Real-time price calculation

### Purchase Process
- Complete customer information form
- Full input validation
- Automatic professional invoice generation
- Data saved to text files

### View History
- Browse all past orders
- View customer details per order
- See order totals and dates
- View specific order items

### Analytics
- Total sales figures
- Order count
- Unique customer count
- Average order value
- Most popular furniture category
- Returning customer identification

## 🔍 Input Validation

All customer inputs are validated for:
- ✓ Non-empty fields
- ✓ Valid email format
- ✓ Valid phone number (10+ digits)
- ✓ Valid name (letters and spaces only)
- ✓ Required address information

## 📝 Notes

- The application is fully self-contained
- No external database required
- Text files ensure easy data backup and portability
- All forms are modal and user-friendly
- Error messages provide clear feedback

## 🎓 Educational Purpose

Developed for ICT2611 - Object-Oriented Programming
Project C: Online Pre-Loved Furniture Store

## 📄 License

Educational Use Only - 2026
