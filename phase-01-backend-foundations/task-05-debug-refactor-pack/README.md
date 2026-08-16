# Task 05 - Debug & Refactor Pack

## Project Description

This project is a refactored version of a simple order calculator.

The original code had input handling, business logic, calculations, and receipt printing inside the `Program` class.

The goal of the refactoring was to improve the code structure, readability, validation, and maintainability without changing the original business behavior.

## Business Rules

The application follows these rules:

- Product price must be positive.
- Quantity must be positive.
- Customer name cannot be empty.
- Product name cannot be empty.
- Regular customers get 0% discount.
- Silver customers get 5% discount.
- Gold customers get 10% discount.
- VIP customers get 15% discount.
- Tax is 14%.
- Shipping costs 50 when the amount after discount is below 1000.
- Shipping is free when the amount after discount is 1000 or more.
- Discount is applied before tax.
- Tax is applied before shipping.

## Project Structure

### Models

- `Customer.cs` - Stores customer information and customer type.
- `CustomerType.cs` - Defines the available customer types.
- `Order.cs` - Stores product, price, and quantity information.

### Services

- `ConsoleMenu.cs` - Handles user input and input validation.
- `OrderCalculator.cs` - Contains the order calculation and business rules.
- `ReceiptPrinter.cs` - Displays the final receipt.

### Program.cs

`Program.cs` is responsible only for connecting the different components together.

## Refactoring Improvements

The following improvements were made:

1. Created a separate `Customer` class.
2. Created a separate `Order` class.
3. Created a `CustomerType` enum instead of using customer type strings everywhere.
4. Moved calculation logic from `Program` to `OrderCalculator`.
5. Moved console input handling to `ConsoleMenu`.
6. Moved receipt output to `ReceiptPrinter`.
7. Renamed unclear variables and methods.
8. Added validation for customer names.
9. Added validation for product names.
10. Added validation for positive prices.
11. Added validation for positive quantities.
12. Added validation for customer types.
13. Replaced magic numbers with named constants.
14. Used `decimal` instead of `double` for money calculations.
15. Separated business logic from console input.
16. Improved the receipt output formatting.
17. Reduced the amount of code inside `Program.cs`.
18. Improved code readability and maintainability.

## Before Refactoring

The original `Program.cs` was responsible for:

- Reading user input.
- Parsing values.
- Calculating subtotal.
- Calculating discounts.
- Calculating tax.
- Calculating shipping.
- Calculating the final total.
- Printing the receipt.

This made the code difficult to maintain and extend.

## After Refactoring

The responsibilities are now separated:

```text
Program
   |
   v
ConsoleMenu
   |
   +---- Customer
   |
   +---- Order
          |
          v
   OrderCalculator
          |
          v
   ReceiptPrinter
