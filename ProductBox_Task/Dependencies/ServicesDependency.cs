using EasyEventPlanning.Services;
using ProductBox_Task.Interfaces;
using ProductBox_Task.Models;

namespace ProductBox_Task.Dependencies
{
    public static class ServicesDependency
    {
        public static void AddServicesDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ICustomerType, CustomerTypeService>();
            services.AddScoped<ICustomers, CustomersService>();
        }
    }
}
