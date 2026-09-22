using Classs_and_object;
using System.Xml.Linq;
/*
MyClass.M1();

int a;
Console.WriteLine("Enter a Number : ");
a = int.Parse(Console.ReadLine());


MyClass.M2(a);
int v = MyClass.M3();



int m, n;
Console.WriteLine("Enter a Number for m: ");
m = int.Parse(Console.ReadLine());


Console.WriteLine("Enter a Number for n: ");
n = int.Parse(Console.ReadLine());

MyClass myClass = new MyClass();

int sum = myClass.M4(m, n);

Console.WriteLine("Sum : " + sum); 

MyClass myClass = new MyClass();

myClass.Name = "Saad";
myClass.Age = 30;

Console.WriteLine("Institute Details");
myClass.DisplayInstituteInfo(); */

int stuId;
string stuName;
Console.WriteLine("Enter StudentId : ");
stuId = int.Parse(Console.ReadLine());


Console.WriteLine("Enter Student Name : ");
stuName = Console.ReadLine();

MyClass myClass = new MyClass(stuId, stuName);

Console.WriteLine("Student Details ");
myClass.DisplayStudentInfo();

Console.ReadLine();  