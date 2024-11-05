using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EventCalendar
{
    /// <summary>
    /// An event has an invitor, a title, a date and an array of
    /// participants in a fixed size depending on the specified
    /// maximum number of participants.
    /// Only title and date of the event can be changed, after an
    /// event has been created.
    /// It is possible to register and unregister persons and to 
    /// cancel an event.
    /// </summary>
    public class Event
    {
        public Person Invitor { get; }
        public int MaxParticipants { get; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        private List<Person> _people;

        public Event(string title, DateTime date, Person invitor, int maxParticipants)
        {
            Title = title;
            MaxParticipants = maxParticipants;
            Date = date;
            Invitor = invitor;

            _people = new();
        }

        /// <summary>
        /// Registers a person for the event. Only successfully registers, if the MaxParticipants value is not reached and the person is not currently registered.
        /// A Person can only attend five event at a time.
        /// </summary>
        /// <param name="person">The person to register</param>
        /// <returns>True, if registry was successful, False otherwise</returns>
        public bool AddPerson(Person person)
        {
            if (_people.Count >= MaxParticipants || _people.Contains(person) || person.EventsAttended >= 5) return false;

            person.EventsAttended++;
            _people.Add(person);
            return true;
        }

        /// <summary>
        /// Unregisters a person from the event. Only successfully unregisters, if the person is already registered.
        /// </summary>
        /// <param name="person">The Person to unregister</param>
        /// <returns>True, if the person could leave successfully, False otherwise</returns>
        public bool RemovePerson(Person person)
        {
            bool isRegistered = _people.Contains(person);
            if (!isRegistered) return false;

            _people.Remove(person);
            return true;
        }

        public Person[] GetSortedParticipants()
        {
            return _people
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToArray();
        }
        
        public override string ToString()
        {
            string people = string.Join(", ", _people);
            return $"{Title}, {Date:dd.MM.yyyy hh.mm}, {Invitor}: {people}";
        }
        
        /// <summary>
        /// Imports events and persons from CSV file, given as an array of strings.
        /// The CSV file has the following format:
        /// EventName; Initial Date; Max Participants; Invitor Last Name; Invitor First Name; Participants
        /// The participants are separated by a comma and are given as a list of last name and first name separated by a space.
        /// Example:
        /// Mary's Birthday; 2021-12-24; 10; Smith; Mary; Doe John, Doe Jane, Doe Jack
        /// If an event contains a Person that is not in the list of persons, the event is skipped.
        /// </summary>
        public static Event[] Import(string[] csv, Person[] persons)
        {
            // EventName; InitialDate; MaxParticipants; InvitorLastName; InvitorFirstName; Participants
            if (csv.Length == 0) return [];

            Event[] result = new Event[csv.Length];
            int valid = 0;

            for (var i = 0; i < csv.Length; i++)
            {
                string[] parts = csv[i].Split("; ");
                
                if (parts.Length != 6) continue;

                string title = parts[0];
                if (!DateTime.TryParseExact(parts[1], "yyyy-MM-dd", null, DateTimeStyles.AllowWhiteSpaces, out var date)) continue;
                if (!int.TryParse(parts[2], out int maxParticipants)) continue;

                if (!TryGetPerson(parts[4], parts[3], persons, out Person invitor)) continue;
                
                Event ev = new Event(title, date, invitor, maxParticipants);
                
                
                // Handle participants
                string[] participants = parts[5].Split(", ");
                bool participantsValid = true;
                
                for (var j = 0; j < participants.Length; j++)
                {
                    string[] names = participants[j].Split(' ');
                    
                    participantsValid &= TryGetPerson(names[1], names[0], persons, out var p);
                    participantsValid &= ev.AddPerson(p);
                }
                
                if (!participantsValid) continue;

                result[valid++] = ev;
            }

            Array.Resize(ref result, valid);
            return result;
        }

        private static bool TryGetPerson(string firstName, string lastName, Person[] validPeople, out Person person)
        {
            person = validPeople.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName, null);
            return person != null;
        }
    }
}