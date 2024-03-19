using System;
using NUnit.Framework;

namespace AddressBook.Tests
{
    [TestFixture]
    public class AddressBookTests
    {
        private AddressBook addressBook;
        private string testFilePath = "C:\\temp\\contact.txt";

        [SetUp]
        public void SetUp()
        {
            addressBook = new AddressBook(testFilePath);
        }

        [TearDown]
        public void TearDown()
        {
            if (System.IO.File.Exists(testFilePath))
            {
                System.IO.File.Delete(testFilePath);
            }
        }

        [Test]
        public void AddContact_ContactAddedToList()
        {
            // Arrange
            var contact = new Contact
            {
                Name = "Test Person",
                PhoneNumber = "123456789",
                EmailAddress = "test@example.com",
                Address = "Test Address"
            };

            // Act
            addressBook.AddContact(contact);

            // Assert
            Assert.AreEqual(contact.EmailAddress, addressBook.DisplayContact("test@example.com"));
        }
    }
}
