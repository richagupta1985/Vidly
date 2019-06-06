using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        //Get/api/customer
        private ApplicationDbContext _Context;
        public CustomersController()
        {
            _Context = new ApplicationDbContext();
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return _Context.Customers.ToList();
        }
        public Customer GetCustomer(int CustomerId)
        {
            var customer = _Context.Customers.FirstOrDefault(x => x.Id == CustomerId);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return customer;
        }
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _Context.Customers.Add(customer);
            _Context.SaveChanges();
            return customer;
        }
        [HttpPut]
        public void UpdateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var oldCustomer = _Context.Customers.FirstOrDefault(x => x.Id == customer.Id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            oldCustomer.Birthdate = customer.Birthdate;
            oldCustomer.MembershipType = customer.MembershipType;
            oldCustomer.Name = customer.Name;
            _Context.SaveChanges();
        }
        [HttpDelete]
        public void DeleteCustomer(int customerId)
        {
            var customer = _Context.Customers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _Context.Customers.Remove(customer);
            _Context.SaveChanges();
        }
    }
}
