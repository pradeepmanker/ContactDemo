using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactDemo.Models;
using ContactDemo.Models.Validations;
using System.Threading.Tasks;

namespace ContactDemo.Repository
{
    public interface IContactUIManager
    {
        ContactMaster CreateContact();
        ApiResponse CreateContact(ContactMaster contact);
        ContactMaster EditContact(Int64 id);
        ApiResponse EditContact(Int64 id, ContactMaster contact);
        ContactMaster Delete(Int64 id);
        bool Delete(Int64 id, ContactMaster contact);
        ContactMaster GetContact(Int64 id);
        IEnumerable<ContactMaster> GetAllContacts();
    }
}