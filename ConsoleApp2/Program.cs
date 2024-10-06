using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankTransactionExample
{
    class Program
    {
        static void Main(string[] args)
        {
          
            
        }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }

        public ICollection<StudentGroup> StudentGroups { get; set; }
    }

    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public ICollection<StudentGroup> StudentGroups { get; set; }
    }

    public class StudentGroup
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }

}
