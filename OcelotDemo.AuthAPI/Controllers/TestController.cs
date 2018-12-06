using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OcelotDemo.AuthAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [Route("GetTestData")]
        [HttpGet]
        public IActionResult Get()
        {
            return  Ok(new string[] { "Erandika", "Sandaruwan" });
        }
    }
}