using Inheritance;

int empId;
string empName, department;
decimal salary;

Console.WriteLine("Enter Employee Id : ");
empId = int.Parse(Console.ReadLine());

Console.WriteLine("Enter Employee Name : ");
empName = (Console.ReadLine());

Console.WriteLine("Enter Employee Department : ");
department = (Console.ReadLine());

Console.WriteLine("Enter Employee Salary : ");
salary = decimal.Parse(Console.ReadLine());

//ClassName objectName = new ClassName();

//ParentClassName objectName = new ChildClassName();

IEmployee employee = new EmployeeDetails(empId, empName, department, salary);

employee.DisplayEmployeeDetails();
Console.WriteLine("Annual Salary :" + employee.CalculateSalary());

EmployeeDetails employeeDetails = (EmployeeDetails)employee;
Console.WriteLine($"MonthlySalary: {employeeDetails.Monthlysalary}");
Console.ReadLine();