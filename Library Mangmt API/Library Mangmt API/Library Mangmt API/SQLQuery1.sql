CREATE DATABASE LibraryDB;
GO
USE LibraryDB;
CREATE TABLE Books (
    Id INT PRIMARY KEY,
    Title NVARCHAR(100),
    Author NVARCHAR(100),
    IsAvailable BIT
);