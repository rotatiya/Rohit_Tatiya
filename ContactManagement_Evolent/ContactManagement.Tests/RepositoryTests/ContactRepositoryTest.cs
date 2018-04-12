using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactManagemet.Models.Repository;
using ContactManagemet.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactManagemet.Tests.RepositoryTests
{
    [TestClass]
    public class ContactRepositoryTest
    {
        private IContactRepository Repo;
        private static int lastContactId;

        public ContactRepositoryTest()
        {
            Repo = new ContactRepository();
        }

        [TestMethod]
        public void ShouldAddContacts()
        {
            Contact contact = new Contact();
            contact.FirstName = "Rohit";
            contact.LastName = "Tatiya";
            contact.Email = "rohit_tatiya1988@yahoo.co.in";
            contact.PhoneNumber = "9405869492";
            contact.Status = "Active";
            Contact dbContact = Repo.AddContact(contact);
            Assert.AreEqual(dbContact, contact);
            lastContactId = dbContact.Id;
        }

        [TestMethod]
        public void ShouldListContacts()
        {
            List<Contact> contacts = Repo.ListContact();
            Assert.IsNotNull(contacts);
            if(contacts != null)
            Assert.IsTrue(contacts.Count > 0);
        }
        [TestMethod]
        public void ShouldUpdateContacts()
        {
            Contact contact = Repo.GetContact(lastContactId);
            contact.FirstName = "Rohitkumar";
            contact.LastName = "Jain";
            contact.Email = "rohit.tatiya1988@gmail.com";
            contact.PhoneNumber = "7387440343";
            contact.Status = "Inactive";

            Contact dbContact = Repo.UpdateContact(contact);
            Assert.AreEqual(dbContact.FirstName, contact.FirstName);
            Assert.AreEqual(dbContact.LastName, contact.LastName);
            Assert.AreEqual(dbContact.Email, contact.Email);
            Assert.AreEqual(dbContact.PhoneNumber, contact.PhoneNumber);
            Assert.AreEqual(dbContact.Status, contact.Status);
        }

        [TestMethod]
        public void ShouldDeleteContacts()
        {
            Assert.IsTrue(Repo.DeleteContact(lastContactId));
            Contact contact = Repo.GetContact(lastContactId);
            Assert.IsNull(contact);
        }

        [TestMethod]
        public void ValidateFirstName()
        {
            Contact contact = new Contact();
            contact.FirstName = string.Empty;
            contact.LastName = "Tatiya";
            contact.Email = "rohit_tatiya1988@yahoo.co.in";
            contact.PhoneNumber = "9405869492";
            contact.Status = "Active";
            ValidateProperty(contact, "The First Name field is required.");            
        }

        [TestMethod]
        public void ValidateLastName()
        {
            Contact contact = new Contact();
            contact.FirstName = "Rohit";
            contact.LastName = string.Empty;
            contact.Email =  "rohit_tatiya1988@yahoo.co.in";
            contact.PhoneNumber = "9405869492";
            contact.Status = "Active";
            ValidateProperty(contact, "The Last Name field is required.");
        }

        [TestMethod]
        public void ValidateEmail()
        {
            Contact contact = new Contact();
            contact.FirstName = "Rohit";
            contact.LastName = "Tatiya";
            contact.Email = string.Empty;
            contact.PhoneNumber = "9405869492";
            contact.Status = "Active";
            ValidateProperty(contact, "The Email field is required.");
        }

        [TestMethod]
        public void ValidatePhone()
        {
            Contact contact = new Contact();
            contact.FirstName = "Rohit";
            contact.LastName = "Tatiya";
            contact.Email =  "rohit_tatiya1988@yahoo.co.in";
            contact.PhoneNumber = "";
            contact.Status = "Active";
            ValidateProperty(contact, "The Phone Number field is required.");
        }

        [TestMethod]
        public void ValidateStatus()
        {
            Contact contact = new Contact();
            contact.FirstName = "Rohit";
            contact.LastName = "Tatiya";
            contact.Email =  "rohit_tatiya1988@yahoo.co.in";
            contact.PhoneNumber = "9405869492";
            contact.Status = "";
            ValidateProperty(contact, "The Status field is required.");
        }

        public void ValidateProperty(Contact contact, string ExpectedValidationMessage)
        {
            ValidationContext context = new ValidationContext(contact);
            List<ValidationResult> errors = new List<ValidationResult>();
            bool IsSuccess = Validator.TryValidateObject(contact, context, errors, true);
            Assert.IsFalse(IsSuccess);
            Assert.IsTrue(errors[0].ErrorMessage == ExpectedValidationMessage);
        }

    }
}
