using System;
using System.Collections.Generic;
using System.Text;

namespace Classs_and_object
{
    public class MyClass
    {
        /* int empId;
         string name;

         public MyClass(int empId, string name)
         {
             this.empId = empId;
             this.name = name;
         } 


         public static void M1()
         {

         }

         public static void M2(int a)
         {
             Console.WriteLine("The Number for 'a' is : " + a);
         }

         public static int M3()
         {
             int v = 0;
             return v;
         } 
         public int M4(int m, int n)
         {
             int sum = m + n;
             return sum;
         } 

        private string name;
        private int age;

        public string Name
        {
            get { return name;  }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if(value >= 0)
                {
                    age = value;
                }
                else
                {
                    Console.WriteLine("Age can not be negative");
                }
            }
        }


        public void DisplayInstituteInfo()
        {
            Console.WriteLine("Name :" + name + "  Age : " + age);
        }
        */

        public int StuId { get; set; }

        public string StuName { get; set; }

        public MyClass(int stuId, string stuName)
            {
               this.StuId = stuId;
               this.StuName = stuName;
            }
        public void DisplayStudentInfo()
        {
            Console.WriteLine("StuId :" + StuId + " " + "StudentName : " + StuName);
        }

    }
}
