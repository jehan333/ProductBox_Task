using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductBox_Task.Interfaces;
using ProductBox_Task.Models;

namespace ProductBox_Task.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private ICustomers _customer;
        private ICustomerType _customerType;
        private Conn_DBContext _db;
        public CustomerController(ILogger<CustomerController> logger, Conn_DBContext db,ICustomerType type,ICustomers customers)
        {
            _customer = customers;
            _customerType = type;
            _logger = logger;
            this._db = db;
        }
      
        #region CustomerType
        [HttpPost]
        [Route("/AddCustomerType")]
        public async Task<IActionResult> AddCustomerType(VM_CustomerType customertype)
        {
            DbResult result = new DbResult();
            if (ModelState.IsValid)
            {
                var check = _db.CustomerTypes.Where(c => c.Name ==  customertype.Name).Any();
                if(check == false)
                {
                    var cusType = new CustomerType()
                    {
                        Name = customertype.Name,
                    };
                    result.msg = _customerType.Insert(cusType);
                    if (result.msg == "")
                        result.msg = _customerType.SaveChanges();
                }
                else
                {
                    result.msg = "Already Entered";
                }
               
                

                return Ok(result.msg);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/GetCustomerType")]
        public async Task<IActionResult> GetCustomerType()
        {
            return Ok(_customerType.GetAll());
        }
        [HttpPut]
        [Route("/UpdateCustomerType/{Id:int}")]
        public async Task<IActionResult> UpdateCustomerType([FromRoute] int Id, VM_CustomerType customertype)
        {
            DbResult result = new DbResult();
            if (ModelState.IsValid)
            {
                var typeName = _customerType.GetById(Id);
                if (typeName != null)
                {
                    typeName.Name= customertype.Name;
                    result.msg = _customerType.SaveChanges();

                    return Ok(result.msg);
                }
                else { return NotFound(); }
                
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/DeleteCustomerType/{Id:int}")]
        public async Task<IActionResult> DeleteCustomerType([FromRoute] int Id)
        {
            DbResult result = new DbResult();
            var type = _customerType.GetById(Id);
            if (type != null)
            {
                result.msg=_customerType.Delete(Id);
                if (result.msg == "")
                    result.msg =_customerType.SaveChanges();

                return Ok(result.msg);
            }
            return NotFound();
        }
        #endregion

        #region customer

        [HttpPost]
        [Route("/AddCustomer")]
        public async Task<IActionResult> AddCustomer(VM_Customer customer)
        {
            DbResult result = new DbResult();
            var cust = new Customer();
            if (ModelState.IsValid)
            {
                var check = _db.Customers.Where(c => c.Name == customer.Name).Any();
                if (check == false)
                {
                    if (customer.Name != null)
                     cust.Name = customer.Name;

                    cust.CustomerTypeId = customer.CustomerTypeId;
                    cust.Description = customer.Description;
                    cust.Address = customer.Address;
                    cust.City = customer.City;
                    cust.State = customer.State;
                    cust.Zip = customer.Zip;
                    cust.LastUpdated = customer.LastUpdated; 

                  result.msg = _customer.Insert(cust);
                   if (result.msg == "")
                   {
                     result.msg = _customer.SaveChanges();
                    }
                 }
              else
              {
                 result.msg = "Already Entered";
               }
                return Ok(result.msg);
            }
            else
            {
                return BadRequest();
            }
        }  
        [HttpGet]
        [Route("/GetCustomer")]
        public async Task<IActionResult> GetCustomer()
        {
            return Ok(_customer.GetAll());
        }

        [HttpPut]
        [Route("/UpdateCustomer/{Id:int}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int Id, VM_Customer customer)
        {
            DbResult result = new DbResult();
            if (ModelState.IsValid)
            {
                var customers = _customer.GetById(Id);
                if (customers != null)
                {
                    customers.Name = customer.Name;
                    customers.CustomerTypeId = customer.CustomerTypeId;
                    //customers.CustomerTypeId = customer.CustomerTypeId;
                    customers.Description = customer.Description;
                    customers.Address = customer.Address;
                    customers.City = customer.City;
                    customers.State = customer.State;
                    customers.Zip = customer.Zip;
                    customers.LastUpdated = customer.LastUpdated;
                    result.msg = _customer.SaveChanges();

                    return Ok(result.msg);
                }
                else { return NotFound(); }

            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("/DeleteCustomer/{Id:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int Id)
        {
            DbResult result = new DbResult();
            var type = _customer.GetById(Id);
            if (type != null)
            {
                result.msg = _customer.Delete(Id);
                if (result.msg == "")
                    result.msg = _customer.SaveChanges();

                return Ok(result.msg);
            }
            return NotFound();
        }
        #endregion
    }
}
