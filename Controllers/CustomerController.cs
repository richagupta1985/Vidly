using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using System.Data.Entity;
namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private ApplicationDbContext _Context;
        public CustomerController()
        {
            _Context = new ApplicationDbContext();
        }
        //protected override void Dispose(bool disposing)
        //{
        //    _Context.Dispose();
        //}
        public ActionResult Index()
        {
            var customers = _Context.Customers.Include(c=>c.MembershipType).ToList();
            //CustomerViewModel customerViewModel = new CustomerViewModel
            //{
            //    Customers = GetCustomers()
            //};
            return View(customers);
        }
        //[Route("Customer/CustomerName/{id}")]
        public ActionResult Details(int id)
        {
            var customer = _Context.Customers.Include(c => c.MembershipType).FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                return View(customer);
            }
            return HttpNotFound();
        }
        //public ActionResult New()
        //{
        //    var membershipType = _Context.MembershipTypes.ToList();
        //    var viewModel = new CustomerFormViewModel
        //    {
        //        MembershipTypes = membershipType
        //    };
        //    return View("CustomerForm",viewModel);
        //}
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                if (customer.MembershipType == null)
                {
                    customer.MembershipType = _Context.MembershipTypes.FirstOrDefault(x => x.Id == customer.MembershipTypeId);
                }
                var viewModel = new CustomerFormViewModel
                {
                    MembershipTypes = _Context.MembershipTypes.ToList(),
                    Customer = customer
                };

                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
                _Context.Customers.Add(customer);
            else
            {
                var customerInDb = _Context.Customers.SingleOrDefault(x => x.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _Context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        public ActionResult Edit(int id)
        {
            var customer = _Context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _Context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }
    }
}