using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OcelotDemo.Department.ViewModels;

namespace OcelotDemo.Department.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var employees = new List<DepartmentViewModel>()
            {
                new DepartmentViewModel(){ Id=1,Name="Human Resource"},
                new DepartmentViewModel(){ Id=2,Name="Marketing"},
                new DepartmentViewModel(){ Id=3,Name="Accounts"},
                new DepartmentViewModel(){ Id=4,Name="Engineering"}
            };
            return Ok(employees);
        }
    }
}