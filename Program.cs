using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApplication1 {
    using System.Linq;
    using System.Collections;

    public class Person 
        // Used to store individuals in the DB
    {
        
        public string Name;

        public int Age; // age stored as a number might help to group customers/persons(on age basis) with ease. 

        public string Gender;

        public string DadName;

        public string MomName;
    }

    // Interface for DI
    public interface IO {
        void WriteLine(string arg);

        string ReadLine();
    }

    // Service implementation
    public struct ConsoleIO : IO {
        public void WriteLine(string arg)
        {
            Console.WriteLine(arg);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }

    public sealed class TestIO : IO
    {
        public void WriteLine(string arg)
        {
            Debug.WriteLine(arg);
        }

        public string ReadLine()
        {
            return string.Empty;
        }
    }

    public static class Db
    {
        public static List<Person> people = new List<Person>();
    }

    public class Solution
    {




        public static void Main(string[] args) {
            var io = new ConsoleIO();
            var p = new Solution();

                for (int i = 0; i < 3; i++) {
                    Db.people.Add(p.ReadPerson(io));
                }

                for (int i = 0; i < Db.people.Count; i++) {
                    p.WritePerson(io, Db.people[i]);
                }

            Console.Read();
        }

        public Person ReadPerson(ConsoleIO io) {
            Person p = new Person();
            io.WriteLine("What is your first name ?");
            string f = Console.ReadLine();
            io.WriteLine("What is your last name ?");
            string l = Console.ReadLine();
            io.WriteLine("What is your age ?");
            var a = Console.ReadLine();

            // M or F
            io.WriteLine("What is your gender ?");
            var g = Console.ReadLine();
            io.WriteLine("What is your dad's name ?");
            var d = Console.ReadLine();
            io.WriteLine("What is your mom's name ?");
            var m = Console.ReadLine();

            p = new Person();
            p.Name = f + " " + l;
            p.Age = a;
            p.Gender = g;
            p.DadName = d;
            p.MomName = m;
            return p;
        }

        public void WritePerson(ConsoleIO io, Person p) {
            var names = p.Name.Split(' ');
            io.WriteLine("Your first name is: {0}", names[0]);
            io.WriteLine("Your last name is: {0}", names[1]);
            io.WriteLine("Your age is: {0}", p.Age);
            io.WriteLine("You are a: {0}", p.Gender == "M" ? "Man" : "Woman");

            var dad = Db.people.Where(o => o.Name == p.DadName);
            /* Here we show the age when there is only one person with p's dad/mom name. 
            If there are multiple records we cannot assure that the age being returned is their parent's age. */ 
            if (dad.Count() == 1) {
                io.WriteLine("Your dad's age is {0}", dad.First().Age);
            }

            var mom = from x in Db.people where x.Name == p.MomName select x;

            if (mom.Count() == 1) {
                io.WriteLine("Your mom's age is {0}", mom.First().Age);
            }
        }
    }
}
