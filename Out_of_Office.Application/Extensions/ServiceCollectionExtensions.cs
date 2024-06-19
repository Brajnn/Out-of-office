using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Out_of_Office.Application.Employee.Queries.GetAllEmployees;
using Out_of_Office.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllEmployeesQuery));
            services.AddAutoMapper(typeof(OfficeMappingProfile));
        }

    }
}
