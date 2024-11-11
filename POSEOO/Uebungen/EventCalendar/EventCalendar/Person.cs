using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCalendar
{
    /// <summary>
    /// A person has a mandatory full name and
    /// an optional email address and phone number.
    /// </summary>
    public class Person
    {
        public const string OPTIONAL_DEFAULT = null;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public int EventsAttended { get; set; }

        public Person(string firstName, string lastName, string eMail = OPTIONAL_DEFAULT, string phoneNumber = OPTIONAL_DEFAULT)
        {
            FirstName = firstName;
            LastName = lastName;
            EMail = eMail;
            PhoneNumber = phoneNumber;

            EventsAttended = 0;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }

        public string ToShortString()
        {
            return $"{LastName} {FirstName[0]}.";
        }

        /// <summary>
        ///  Import a list of persons from a CSV string array.
        /// Format:
        /// FirstName; LastName; Email; Phone
        /// Email and Phone may be empty or even missing.
        /// </summary>
        public static Person[] Import(string[] personCsv)
        {
            if (personCsv.Length == 0) return [];

            Person[] result = new Person[personCsv.Length];
            int valid = 0;

            for (var i = 0; i < personCsv.Length; i++)
            {
                string[] personData = personCsv[i].Replace(" ", "").Split(";");

                // Phone number and email can be missing. Maximum 4 entries
                if (personData.Length is < 2 or > 4) continue;

                // FirstName; LastName; Email; Phone
                string firstName = personData[0];
                string lastName = personData[1];
                string mail = personData.Length > 2 ? personData[2] : OPTIONAL_DEFAULT;
                string phone = personData.Length > 3 ? personData[3] : OPTIONAL_DEFAULT;

                result[valid++] = new Person(firstName, lastName, mail, phone);
            }
            
            Array.Resize(ref result, valid);

            return result;
        }
    }
}