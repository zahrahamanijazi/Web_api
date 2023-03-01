using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Part2.Models
{
    public class clsPerson
    {
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string TellNumber { get; set; }
        public int HouseNumber { get; set; }
        public string ZipCode { get; set; }

        public string ShowTitle
        {
            get
            {
                return PersonName + ", " + Address + " " + HouseNumber.ToString() + ", " + ZipCode + " " + CityName + " Tel: " + TellNumber;
            }
        }
        //=======================================================================================================
        public clsPerson()
        {

        }

        //=======================================================================================================
        public clsPerson(int id, string name, string city, string address, string tell, int no, string zip)
        {
            ID = id;
            PersonName = name;
            CityName = city;
            Address = address;
            TellNumber = tell;
            HouseNumber = no;
            ZipCode = zip;
        }

        //=======================================================================================================
        private static List<clsPerson> PhoneBook;
        private static Dictionary<DateTime, int> DatePerson;
        // this is the phone book in which each person is associated which a different day
        static clsPerson()
        {
            PhoneBook = new List<clsPerson>();
            PhoneBook.Add(new clsPerson(1, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(2, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(3, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(4, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(5, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(6, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(7, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(8, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(9, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));
            PhoneBook.Add(new clsPerson(10, "John Smith", "Stavanger", "Lagardsveien", "51634782", 3, "4010"));

            DatePerson = new Dictionary<DateTime, int>();
            DatePerson.Add(DateTime.Parse("2022-09-09"), 1);
            DatePerson.Add(DateTime.Parse("2022-09-10"), 2);
            DatePerson.Add(DateTime.Parse("2022-09-11"), 3);
            DatePerson.Add(DateTime.Parse("2022-09-12"), 4);
            DatePerson.Add(DateTime.Parse("2022-09-13"), 5);
            DatePerson.Add(DateTime.Parse("2022-09-14"), 6);
            DatePerson.Add(DateTime.Parse("2022-09-15"), 7);
            DatePerson.Add(DateTime.Parse("2022-09-16"), 8);
            DatePerson.Add(DateTime.Parse("2022-09-17"), 9);
            DatePerson.Add(DateTime.Parse("2022-09-18"), 10);
            DatePerson.Add(DateTime.Parse("2022-09-19"), 1);
            DatePerson.Add(DateTime.Parse("2022-09-20"), 2);
            DatePerson.Add(DateTime.Parse("2022-09-21"), 3);
            DatePerson.Add(DateTime.Parse("2022-09-22"), 4);
            DatePerson.Add(DateTime.Parse("2022-09-23"), 5);
            DatePerson.Add(DateTime.Parse("2022-09-24"), 6);
            DatePerson.Add(DateTime.Parse("2022-09-25"), 7);
            DatePerson.Add(DateTime.Parse("2022-09-26"), 8);
            DatePerson.Add(DateTime.Parse("2022-09-27"), 9);
            DatePerson.Add(DateTime.Parse("2022-09-28"), 10);
        }

        //=======================================================================================================
        // Retrive the person's information by date associated to this person in the Phonebook
        public static clsPerson GetPersonByDate(DateTime date)
        {
            int ID = 0;
            if (DatePerson.TryGetValue(date.Date, out ID))
            {
                clsPerson Person = PhoneBook.Where(i => i.ID == ID).DefaultIfEmpty(null).First();
                return Person;
            }
            else
            {
                return null;
            }
        }

        //=======================================================================================================
    }


}