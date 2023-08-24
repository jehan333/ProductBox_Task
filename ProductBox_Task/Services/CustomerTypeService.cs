

using ProductBox_Task.Interfaces;
using ProductBox_Task.Models;

namespace EasyEventPlanning.Services
{
    public class CustomerTypeService : GenericRepository<CustomerType>, ICustomerType
    {

        public CustomerTypeService(Conn_DBContext context) : base(context)
        {
            
        }
    }
}
