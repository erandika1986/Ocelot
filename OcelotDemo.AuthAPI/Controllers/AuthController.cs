using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OcelotDemo.AuthAPI.Data;
using OcelotDemo.AuthAPI.Models;
using OcelotDemo.AuthAPI.ViewModels;

namespace OcelotDemo.AuthAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;


        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,  ApplicationDbContext context, IConfiguration config)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._context = context;
            this._config = config;
        }

        // GET api/values
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var now = DateTime.UtcNow;
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Aud,"employee"),
                        new Claim(JwtRegisteredClaimNames.Aud,"department")
                    };

                    var tokeOptions = new JwtSecurityToken(
                        issuer: _config["Tokens:Issuer"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(100),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
            }

            return Unauthorized();
        }

    }
}