using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Out_of_Office.Domain.Interfaces;
using Out_of_Office.Infrastructure.Presistance;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Out_of_Office.Infrastructure.Repositories;

namespace Out_of_Office.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Out_of_OfficeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OutOfOfficeConnectionString")));
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddScoped<IApprovalRequestRepository, ApprovalRequestRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        }
    }
}
