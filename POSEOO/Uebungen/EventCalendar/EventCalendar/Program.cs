using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace EventCalendar
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool isDemoMode = !CheckIfImportMode(args);
            if (isDemoMode)
            {
                RunDemo();
            }
            else
            {
                RunNormal(args[0], args[1]);
            }
        }

        private static void RunNormal(string personFile, string eventFile)
        {
            string[] personCsv = File.ReadAllLines(personFile)[1..];
            string[] eventCsv = File.ReadAllLines(eventFile)[1..];
            
            Person[] persons = Person.Import(personCsv);
            Event[] events = Event.Import(eventCsv, persons);
            
            Console.WriteLine("Imported {0} person(s) and {1} event(s).", persons.Length, events.Length);

            var sortedEvents = events.OrderBy(e => e.Date);
            Console.WriteLine("\n" + string.Join("\n", sortedEvents));
        }

        private static void RunDemo()
        {
            Console.WriteLine("Creating demo event...");
            Event ev = new Event("Event 1", DateTime.Now, new Person("John", "Doe"), 5);
            Console.WriteLine("Demo event created.");

            Console.WriteLine("\nInviting some people...");
            ev.AddPerson(new Person("Jane", "Doe"));
            Console.WriteLine("Added Jane Doe.");
            ev.AddPerson(new Person("Jack", "Doe"));
            Console.WriteLine("Added Jack Doe.");
            ev.AddPerson(new Person("Jill", "Brown"));
            Console.WriteLine("Added Jill Brown.");

            Console.WriteLine("\nPrinting event values...");
            Console.WriteLine("Title: {0}", ev.Title);
            Console.WriteLine("Date: {0}", ev.Date);
            Console.WriteLine("Invitor: {0}", ev.Invitor);
            Console.WriteLine("Max Participants: {0}", ev.MaxParticipants);
            Console.WriteLine("People: {0}", string.Join<Person>(", ", ev.GetSortedParticipants()));
        }

        public static bool CheckIfImportMode(string[] args)
        {
            return args.Length == 2 && File.Exists(args[0]) && File.Exists(args[1]);
        }
    }
}
