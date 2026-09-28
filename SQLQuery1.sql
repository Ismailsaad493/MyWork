CREATE DATABASE SQLPractice;
GO

USE SQLPractice;
GO

CREATE TABLE Employeess
(
    EmpID INT PRIMARY KEY,
    Name VARCHAR(50),
    Department VARCHAR(50),
    Salary INT,
    City VARCHAR(50)
);

INSERT INTO Employees (EmpID, Name, Department, Salary, City)
VALUES
(1, 'Arun', 'IT', 45000, 'Chennai'),
(2, 'Kumar', 'HR', 35000, 'Bangalore'),
(3, 'Ravi', 'IT', 55000, 'Chennai'),
(4, 'Priya', 'Finance', 50000, 'Bangalore'),
(5, 'Anu', 'IT', 60000, 'Hyderabad'),
(6, 'Rahul', 'HR', 40000, 'Chennai'),
(7, 'Divya', 'Finance', 65000, 'Bangalore'),
(8, 'Vijay', 'IT', 48000, 'Hyderabad');