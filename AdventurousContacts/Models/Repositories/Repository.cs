using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdventurousContacts.Models.Repositories
{
    public class Repository : IRepository
    {
        private bool _disposed = false;

        private ContactEntities _entities = new ContactEntities(); 
        
        public void Add(Contact contact)
        {

            _entities.uspAddContact_SELECT(contact.FirstName, contact.LastName, contact.EmailAddress); 
        }

        public void Delete(Contact contact)
        {
            if (_entities.Entry(contact).State == EntityState.Detached)
            {
                _entities.Contacts.Attach(contact);
            }

            _entities.Contacts.Remove(contact);
        }

        public IEnumerable<Contact> FindAllContacts()
        {
            return _entities.Contacts.ToList(); 
        }

        public Contact GetContactById(int contactId)
        {
            return _entities.Contacts.Find(contactId);
        }

        public List<Contact> GetLastContacts(int count = 20)
        {
            var lastContacts = (from contact in _entities.Contacts select contact).OrderByDescending(contact => contact.ContactID).Take(count);
            return lastContacts.ToList();
        }

        public void Save()
        {
            _entities.SaveChanges();
        }

        public void Update(Contact contact)
        {
             if (_entities.Entry(contact).State == EntityState.Detached)
            {
                _entities.Contacts.Attach(contact);
            }

            _entities.Entry(contact).State = EntityState.Modified;
        
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}