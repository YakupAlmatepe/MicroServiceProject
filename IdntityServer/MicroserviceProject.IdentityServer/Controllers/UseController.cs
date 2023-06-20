using MicroserviceProject.IdentityServer.Dtos;
using MicroserviceProject.IdentityServer.Models;
using MicroServiceProject.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MicroserviceProject.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UseController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser()
            {
                Email = signUpDto.Email,
                UserName = signUpDto.UserName,
                UserCity = signUpDto.City,
                UserCountry = signUpDto.Country
            };
            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userClaimID = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);//token isteyecek ve çözecek 
            var user = await _userManager.FindByIdAsync(userClaimID.Value);
            return Ok(new { Id = user.Id, username = user.UserName, email = user.Email, city = user.UserCity, country = user.UserCountry });
        }
    }
}
    

