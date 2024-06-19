using AutoMapper;
using Out_of_Office.Application.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Mapping
{
    public class OfficeMappingProfile: Profile
    {
        public OfficeMappingProfile()
        {
            CreateMap<Domain.Entities.Employee, EmployeeDto>();
        }
    }
}
