using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AdventurousContacts.Models; 
using AdventurousContacts.Models.Repositories;
using System.ComponentModel;
using System.Data;
using System.Net;


namespace AdventurousContacts.Controllers
{
    public class ContactController : Controller
    {
        private IRepository _repository;

        public ContactController() : this(new Repository())
        {
        }
        public ContactController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.GetLastContacts());
        }

        public ActionResult Create()
        {
            return View(); 
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName, LastName, EmailAddress")]Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Add(contact);
                    _repository.Save();
                    TempData["sucess"] = contact.FirstName + " " + contact.LastName + " " + "(" + contact.EmailAddress + ") was successfully created.";
                    return View("Sucess");
                }
            }
            catch (DataException)
            {
                TempData["error"] = "Create was unsucessful. Please corecct any errors and try again.";
            }

            return View(contact);

        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return View("Error");
            }

            var contact = _repository.GetContactById(id.Value);

            if (contact == null)
            {
                return View("ContactNotFound");
            }

            return View(contact);
        }


        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var contact = new Contact { ContactID = id };
                _repository.Delete(contact);
                _repository.Save();
                TempData["sucess"] = "Contact is successfully deleted.";
                return View("Sucess");

            }
            catch (DataException)
            {
                TempData["error"] = "Delete was unsucessfull.";
            }

            return View();
        }
        public ActionResult Edit(int? id)
        {
            
            if (!id.HasValue)
            {
                return View("Error");
            }

            var contact = _repository.GetContactById(id.Value);

            if (contact == null)
            {
                return View("ContactNotFound");
            }

            return View(contact);
        
        }

        [HttpPost, ActionName("Edit"), ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (contact == null)
            {
                return View("ContactNotFound");
            }

            if (TryUpdateModel(contact, new string[] { "FirstName", "LastName", "EmailAddress" }))
            {
                try
                {
                    _repository.Update(contact);
                    _repository.Save();
                    TempData["sucess"] = contact.FirstName + " " + contact.LastName + " " + "(" + contact.EmailAddress + ") was successfully saved.";
                    return View("Sucess");
                }
                catch (DataException)
                {
                    TempData["error"] = "Couldn't edit contact. Please corecct any errors and try again.";
                }
            }

            return View(contact);
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
	}
}