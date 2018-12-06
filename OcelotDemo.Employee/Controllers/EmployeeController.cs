using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OcelotDemo.Employee.ViewModels;

namespace OcelotDemo.Employee.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var employees = new List<EmployeeViewModel>()
            {
                new EmployeeViewModel(){ Id=1,Name="Erandika Sandaruwan"},
                new EmployeeViewModel(){ Id=2,Name="Sujani Piumika"},
                new EmployeeViewModel(){ Id=3,Name="Sudeepa Madushanka"}
            };
            return Ok(employees);
        }
    }
}