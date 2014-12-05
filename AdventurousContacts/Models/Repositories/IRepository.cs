using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventurousContacts.Models.Repositories
{
    public interface IRepository : IDisposable
    {
         void Add(Contact contact);
         void Delete(Contact contact);
        IEnumerable<Contact> FindAllContacts();
         Contact GetContactById(int contactId);
        List<Contact> GetLastContacts(int count = 20);
         void Save();
         void Update(Contact contact); 
    }
}
