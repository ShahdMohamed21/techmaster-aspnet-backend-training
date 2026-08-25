# Task 06 - SQL & ERD Starter

## Selected Scenario

Library Management System

## Main Entities

* Authors
* Categories
* Books
* Members
* BorrowRecords

## Tables and Fields

### Authors

* AuthorId (PK)
* FullName
* BirthDate
* Country

### Categories

* CategoryId (PK)
* Name
* Description

### Books

* BookId (PK)
* Title
* ISBN
* PublishedYear
* AvailableCopies
* AuthorId (FK)
* CategoryId (FK)

### Members

* MemberId (PK)
* FullName
* Email
* PhoneNumber
* JoinDate
* IsActive

### BorrowRecords

* BorrowRecordId (PK)
* BookId (FK)
* MemberId (FK)
* BorrowDate
* DueDate
* ReturnDate
* Status

## Primary Keys

* Authors: `AuthorId`
* Categories: `CategoryId`
* Books: `BookId`
* Members: `MemberId`
* BorrowRecords: `BorrowRecordId`

## Foreign Keys

* `Books.AuthorId` references `Authors.AuthorId`
* `Books.CategoryId` references `Categories.CategoryId`
* `BorrowRecords.BookId` references `Books.BookId`
* `BorrowRecords.MemberId` references `Members.MemberId`

## Relationships

* One Author can have many Books.
* One Category can contain many Books.
* One Member can have many BorrowRecords.
* One Book can have many BorrowRecords.

## Why I Designed It This Way

The database is divided into separate tables based on the main entities in the library system.

Authors and Categories are stored separately because one author or category can be related to many books.

The Books table contains foreign keys to connect each book with its author and category.

Members are stored separately because a member can borrow many books over time.

BorrowRecords stores each borrowing operation and contains the book, member, borrow date, due date, return date, and status.

This design reduces duplicated data and makes the relationships between the entities clear.

The database structure is simple, organized, and suitable for implementation later using Entity Framework Core.

## SQL Queries

The `Task06.sql` file contains the following required queries:

1. Select all books.
2. Select all active members.
3. Select books by category.
4. Count books per category.
5. Select borrow records with member name and book title using JOIN.
6. Select overdue books.
7. Select borrowing history for one member.
8. Select available books.
9. Count how many books each author has.
10. Select top 5 most borrowed books.

## ERD

The ERD shows all five tables, their primary keys, foreign keys, and relationships.

### Relationship Summary

```text
Authors 1 ────< Books
Categories 1 ────< Books
Members 1 ────< BorrowRecords
Books 1 ────< BorrowRecords
```

## Design Decision

The `BorrowRecords` table is used to store each borrowing operation instead of storing borrowing information directly inside the Members or Books tables. This allows the system to keep a complete borrowing history for every member and every book.

## Conclusion

The Library Management System database provides a simple relational structure for managing authors, categories, books, members, and borrowing operations. Primary keys uniquely identify records, while foreign keys maintain the relationships between tables.
