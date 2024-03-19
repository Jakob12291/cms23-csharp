using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;

namespace AddressBook
{

    // Representerar en kontakt med namn, telefonnummer, e-postadress och adress.

    public class Contact
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
    }


    // Representerar en adressbok som lagrar en lista över kontakter.

    public class AddressBook
    {
        private List<Contact> contacts;
        private string filePath;


        // Konstruerar ett nytt AddressBook-objekt.
        //
        // Parametrar:
        // - filePath: Sökvägen till filen där kontakterna ska sparas.

        public AddressBook(string filePath)
        {
            contacts = new List<Contact>();
            this.filePath = filePath;
        }


        // Lägger till en kontakt i adressboken.
        //
        // Parametrar:
        // - contact: Kontakten som ska läggas till.

        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }


        // Tar bort en kontakt från adressboken genom e-postadress.
        //
        // Parametrar:
        // - emailAddress: E-postadressen för kontakten som ska tas bort.

        public void RemoveContact(string emailAddress)
        {
            Contact contactToRemove = contacts.Find(match: c => c.EmailAddress == emailAddress);
            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
            }
        }


        // Listar alla kontakter i adressboken.

        public void ListContacts()
        {
            Console.WriteLine("Kontakter:");
            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"Namn: {contact.Name}");
                Console.WriteLine($"Telefonnummer: {contact.PhoneNumber}");
                Console.WriteLine($"E-postadress: {contact.EmailAddress}");
                Console.WriteLine($"Adress: {contact.Address}");
                Console.WriteLine();
            }
        }

        // Visar detaljerad information om en specifik kontakt.
        //
        // Parametrar:
        // - emailAddress: E-postadressen för kontakten som ska visas.

        public string DisplayContact(string emailAddress)
        {
            Contact contact = contacts.Find(match: c => c.EmailAddress == emailAddress);
            if (contact != null)
            {
                Console.WriteLine($"Namn: {contact.Name}");
                Console.WriteLine($"Telefonnummer: {contact.PhoneNumber}");
                Console.WriteLine($"E-postadress: {contact.EmailAddress}");
                Console.WriteLine($"Adress: {contact.Address}");
            }
            else
            {
                Console.WriteLine("Kontakt hittades inte.");
            }

            return contact.EmailAddress;
        }


        // Sparar kontakterna till en fil.

        public void SaveContacts()
        {
            int i = 0;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Contact contact in contacts)
                {
                    writer.WriteLine($"{i}  Namn : {contact.Name}");
                    writer.WriteLine($"{i}  PhoneNumber: {contact.PhoneNumber}");
                    writer.WriteLine($"{i}  EmailAddress: {contact.EmailAddress}");
                    writer.WriteLine($"{i}  Adress: {contact.Address}");
                    i++;
                }
            }
        }


        // Läser in kontakterna från en fil.

        public void LoadContacts()
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string name = reader.ReadLine();
                        string phoneNumber = reader.ReadLine();
                        string emailAddress = reader.ReadLine();
                        string address = reader.ReadLine();

                        Contact contact = new Contact
                        {
                            Name = name,
                            PhoneNumber = phoneNumber,
                            EmailAddress = emailAddress,
                            Address = address
                        };

                        contacts.Add(contact);
                    }
                }
            }
        }
    }


    // Representerar en enkel konsolapplikation för att hantera en adressbok.

    public class Program
    {
        public static void Main()
        {
            // Skapar en adressbok och specificerar sökvägen till filen där kontakterna ska sparas.
            //den är sparad i temp mapen
            AddressBook addressBook = new AddressBook("C:\\temp\\contact.txt");

            // Läser in kontakter från filen.
            addressBook.LoadContacts();

            // Visar menyalternativ.
            Console.WriteLine("Adressbok");
            Console.WriteLine("1. Lägg till kontakt");
            Console.WriteLine("2. Ta bort kontakt");
            Console.WriteLine("3. Lista kontakter");
            Console.WriteLine("4. Visa kontakt");
            Console.WriteLine("5. Spara kontakter");
            Console.WriteLine("6. Avsluta");

            // Bearbetar användarinput.
            bool exit = false;
            while (!exit)
            {
                Console.Write("Ange alternativnummer: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // Lägg till kontakt
                        Contact newContact = new Contact();
                        Console.Write("Namn: ");
                        newContact.Name = Console.ReadLine();
                        Console.Write("Telefonnummer: ");
                        newContact.PhoneNumber = Console.ReadLine();
                        Console.Write("E-postadress: ");
                        newContact.EmailAddress = Console.ReadLine();
                        Console.Write("Adress: ");
                        newContact.Address = Console.ReadLine();
                        addressBook.AddContact(newContact);
                        Console.WriteLine("Kontakt tillagd.");
                        Console.WriteLine();
                        break;

                    case "2":
                        // Ta bort kontakt
                        Console.Write("Ange e-postadress för kontakten som ska tas bort: ");
                        string emailAddress = Console.ReadLine();
                        addressBook.RemoveContact(emailAddress);
                        Console.WriteLine("Kontakt borttagen.");
                        Console.WriteLine();
                        break;

                    case "3":
                        // Lista kontakter
                        addressBook.ListContacts();
                        Console.WriteLine();
                        break;

                    case "4":
                        // Visa kontakt
                        Console.Write("Ange e-postadress för kontakten som ska visas: ");
                        string displayEmailAddress = Console.ReadLine();
                        addressBook.DisplayContact(displayEmailAddress);
                        Console.WriteLine();
                        break;

                    case "5":
                        // Spara kontakter
                        addressBook.SaveContacts();
                        Console.WriteLine("Kontakter sparade.");
                        Console.WriteLine();
                        break;

                    case "6":
                        // Avsluta
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt alternativ.");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
