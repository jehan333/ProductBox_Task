

using ProductBox_Task.Interfaces;
using ProductBox_Task.Models;

namespace EasyEventPlanning.Services
{
    public class CustomersService : GenericRepository<Customer>, ICustomers
    {

        public CustomersService(Conn_DBContext context) : base(context)
        {
            
        }
    }
}
