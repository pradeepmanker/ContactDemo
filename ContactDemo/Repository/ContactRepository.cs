using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContactDemo.DataModel;
using ContactDemo.Models;

namespace ContactDemo.Repository
{
    public class ContactRepository : IContactRepository
    {
        private ContactStore _entities = new ContactStore();

        // Contact methods

        public ContactMaster GetContact(Int64 id)
        {
            return _entities.ContactMasters.Where(CM => CM.ID == id).FirstOrDefault();
        }

        public IEnumerable<ContactMaster> GetAllContacts()
        {
            return _entities.ContactMasters.ToList();
        }

        public ContactMaster CreateContact(ContactMaster contactToCreate)
        {
            // Save new contact
            try
            {
                _entities.ContactMasters.Add(contactToCreate);
                _entities.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return contactToCreate;
        }

        public ContactMaster EditContact(ContactMaster contactToEdit)
        {
            try
            {

                // Get original contact
                var originalContact = GetContact(contactToEdit.ID);

                // Save changes
                if (originalContact != null)
                {
                    _entities.Entry(originalContact).CurrentValues.SetValues(contactToEdit);
                }
                _entities.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return contactToEdit;
        }

        public void DeleteContact(ContactMaster contactToDelete)
        {
            try
            {
                var originalContact = GetContact(contactToDelete.ID);
                if (originalContact != null)
                {
                    _entities.ContactMasters.Remove(contactToDelete);
                }
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}