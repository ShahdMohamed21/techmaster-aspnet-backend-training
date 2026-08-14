# Task 02 - OOP Bank Account System

## 📌 Overview

A simple console-based banking system built with C# and Object-Oriented Programming (OOP).

The system allows bank employees to create customer accounts and perform basic banking operations such as deposits, withdrawals, and money transfers while validating business rules and recording successful transactions.

## 🎯 Features

* Create customer bank accounts
* Validate customer information
* Deposit money
* Withdraw money
* Transfer money between accounts
* View account details
* View transaction history
* View all accounts
* Validate invalid operations
* Keep transaction records for successful financial operations

## 🏗️ Project Structure

```text
BankAccountSystem/
│
├── Models/
│   ├── Customer.cs
│   ├── BankAccount.cs
│   ├── Transaction.cs
│   ├── AccountType.cs
│   └── TransactionType.cs
│
├── Services/
│   └── BankService.cs
│
├── UI/
│   └── ConsoleMenu.cs
│
└── Program.cs
```

## 🧩 Main Components

### Customer

Stores customer information:

* Customer ID
* Full Name
* Email
* Phone Number
* Created Date

### BankAccount

Represents a customer's bank account and contains:

* Account Number
* Customer
* Balance
* Account Type
* Created Date
* Active Status
* Transaction History

The balance is encapsulated and can only be changed through controlled methods such as `Deposit()` and `Withdraw()`.

### Transaction

Stores information about successful financial operations:

* Transaction ID
* Account Number
* Transaction Type
* Amount
* Transaction Date
* Description
* Balance After Transaction

### BankService

Contains the main banking business logic, including:

* Account creation
* Deposits
* Withdrawals
* Transfers
* Account searching
* Transaction history
* Viewing all accounts

### ConsoleMenu

Provides the console interface that allows employees to interact with the banking system.

## 🖥️ Console Menu

```text
====== TechMaster Bank System ======

1. Create Customer Account
2. Deposit Money
3. Withdraw Money
4. Transfer Money
5. View Account Details
6. View Transaction History
7. View All Accounts
8. Exit
```

## 🔐 Validation & Business Rules

The system validates important banking operations, including:

* Customer information cannot be empty
* Initial balance cannot be negative
* Deposit amount must be positive
* Withdrawal amount must be positive
* Withdrawal cannot exceed the account balance
* Both accounts must exist before transferring money
* Source and destination accounts cannot be the same
* Transfer amount must be valid
* Account numbers and customer IDs must be unique

## 🧠 OOP Concepts Used

This project demonstrates:

* **Encapsulation** – protecting account balance from direct modification
* **Classes & Objects** – modeling customers, accounts, and transactions
* **Methods** – implementing banking behavior
* **Enums** – representing account and transaction types
* **Separation of Concerns** – separating Models, Services, and UI
* **Collections** – storing accounts and transactions in memory

## 🛠️ Technologies

* C#
* .NET
* Console Application
* Object-Oriented Programming
* In-Memory Collections

## ▶️ How to Run

1. Clone the repository.
2. Open the `BankAccountSystem` project in Visual Studio.
3. Build the project.
4. Run the application.
5. Use the console menu to perform banking operations.

## 🧪 Tested Scenarios

The system is designed to handle:

* Creating multiple customer accounts
* Depositing money
* Withdrawing money
* Transferring money between accounts
* Viewing account information
* Viewing transaction history
* Invalid account numbers
* Invalid amounts
* Insufficient balance
* Same-account transfers
* Empty or invalid customer information

## 👩‍💻 Project

**Task 02 - OOP Bank Account System**

Part of the TechMaster Academy ASP.NET Backend Career Training - Phase 01.
