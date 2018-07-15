using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactDemo.Models;
using ContactDemo.Models.Validations;
using System.Text.RegularExpressions;

namespace ContactDemo.Repository
{
    public class ContactService : IContactService
    {
        private IContactRepository _repository;
        private IValidationDictionary _validationDictionary;


        public ContactService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new ContactRepository())
        { }


        public ContactService(IValidationDictionary validationDictionary, IContactRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }

        #region IContactService Members

        public bool CreateContact(ContactMaster contactToCreate)
        {
            // Validation logic
            if (!ValidateContact(contactToCreate))
                return false;

            // Database logic
            try
            {
                _repository.CreateContact(contactToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContact(ContactMaster contactToEdit)
        {
            // Validation logic
            if (!ValidateContact(contactToEdit))
                return false;

            // Database logic
            try
            {
                _repository.EditContact(contactToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteContact(ContactMaster contactToDelete)
        {
            try
            {
                _repository.DeleteContact(contactToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ContactMaster GetContact(Int64 id)
        {
            return _repository.GetContact(id);
        }

        public IEnumerable<ContactMaster> GetAllContacts()
        {
            return _repository.GetAllContacts();
        }

        #endregion

        // Contact methods

        public bool ValidateContact(ContactMaster contactToValidate)
        {
            if (contactToValidate.FirstName == null || contactToValidate.FirstName.Trim().Length == 0)
                _validationDictionary.AddError("FirstName", "First name is required.");
            else if(contactToValidate.FirstName == null || contactToValidate.FirstName.Trim().Length > 50)
                _validationDictionary.AddError("FirstName", "First name cannot be longer than 50 characters.");

            if (contactToValidate.LastName == null || contactToValidate.LastName.Trim().Length == 0)
                _validationDictionary.AddError("LastName", "Last name is required.");
            else if (contactToValidate.LastName == null || contactToValidate.LastName.Trim().Length > 50)
                _validationDictionary.AddError("LastName", "Last name cannot be longer than 50 characters.");

            if (contactToValidate.Phone == null || contactToValidate.Phone.Trim().Length == 0)
                _validationDictionary.AddError("Phone", "Phone is required.");
            else if (contactToValidate.Phone == null || contactToValidate.Phone.Length > 0 && !Regex.IsMatch(contactToValidate.Phone, @"^(\+\d{1,3}[- ]?)?\d{10}$"))
                _validationDictionary.AddError("Phone", "Invalid phone number.");
            else if (contactToValidate.Phone == null || contactToValidate.Phone.Trim().Length > 20)
                _validationDictionary.AddError("Phone", "Phone cannot be longer than 20 characters.");

            if (contactToValidate.Email == null || contactToValidate.Email.Trim().Length == 0)
                _validationDictionary.AddError("Email", "Email is required.");
            else if (contactToValidate.Email == null || contactToValidate.Email.Length > 0 && !Regex.IsMatch(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                _validationDictionary.AddError("Email", "Invalid email address.");
            else if (contactToValidate.Email == null || contactToValidate.Email.Trim().Length > 150)
                _validationDictionary.AddError("Email", "Email cannot be longer than 150 characters.");

            return _validationDictionary.IsValid;
        }
    }
}