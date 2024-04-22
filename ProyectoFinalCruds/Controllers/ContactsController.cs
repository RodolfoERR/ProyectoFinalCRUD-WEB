using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalCruds.Data;
using ProyectoFinalCruds.Models;

namespace ProyectoFinalCruds.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var contacts = _context.contacts
                               .Include(o => o.Customer)
                               .OrderBy(c => c.CONTACT_ID);

            var paginatedContacts = contacts.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalContacts = _context.contacts.Count();
            int totalPages = (int)Math.Ceiling((double)totalContacts / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedContacts);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _context.contacts.FirstOrDefault(c => c.CONTACT_ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        public ActionResult Create()
        {
            ViewBag.Customers = _context.customers
                .Select(c => new SelectListItem
                {
                    Value = c.CUSTOMER_ID.ToString(),
                    Text = c.NAME
                })
                .ToList();
            return View(new Contacts());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contacts contacts)
        {
            if (!ModelState.IsValid)
            {
                _context.contacts.Add(contacts);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Customers = _context.customers
                .Select(c => new SelectListItem
                {
                    Value = c.CUSTOMER_ID.ToString(),
                    Text = c.NAME
                })
                .ToList();
            return View(contacts);
        }
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var cust = _context.contacts.Find(id);
            return View(cust);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contacts contact)
        {
            if (!ModelState.IsValid)
            {
                _context.contacts.Update(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _context.contacts.FirstOrDefault(c => c.CONTACT_ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var contact = _context.contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.contacts.Remove(contact);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
