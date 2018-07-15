using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactDemo.Models;

namespace ContactDemo.Repository
{
    public interface IContactRepository
    {
        ContactMaster CreateContact(ContactMaster contactToCreate);
        void DeleteContact(ContactMaster contactToDelete);
        ContactMaster EditContact(ContactMaster contactToEdit);
        ContactMaster GetContact(Int64 id);
        IEnumerable<ContactMaster> GetAllContacts();
    }
}
