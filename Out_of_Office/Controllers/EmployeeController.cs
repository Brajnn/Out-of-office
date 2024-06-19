using MediatR;
using Microsoft.AspNetCore.Mvc;
using Out_of_Office.Application.Employee.Queries.GetAllEmployees;
namespace Out_of_Office.Controllers
{
    public class EmployeeController :Controller
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            return View(employees); 
        }
    }
}
