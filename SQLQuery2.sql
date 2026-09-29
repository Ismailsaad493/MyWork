USE SQLPractice;

/*SELECT * from Employees WHERE Department =  'IT' ;

SELECT * from Employees WHERE Salary <50000  ;

SELECT * from Employees WHERE Department =  'IT' and Salary > 50000 ;

SELECT * from Employees WHERE Department =  'IT' OR Department = 'HR' */

SELECT Department, AVG(Salary)
FROM Employees
WHERE Salary > 40000
GROUP BY Department
ORDER BY AVG(Salary) DESC;
