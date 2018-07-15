using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactDemo.Models;

namespace ContactDemo.Repository
{
    public interface IContactService
    {
        bool CreateContact(ContactMaster contactToCreate);
        bool DeleteContact(ContactMaster contactToDelete);
        bool EditContact(ContactMaster contactToEdit);
        ContactMaster GetContact(Int64 id);
        IEnumerable<ContactMaster> GetAllContacts();
    }
}
