using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        //Injection User Manager
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        //Register /api/Auth/Register POST
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.UserName
            };

            var identityResult =  await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult.Succeeded)
            {
                //  Add roles to this User
                if (request.Roles != null && request.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, request.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered, please login.");
                    }
                }
            }        
                return BadRequest("Something went wrong");        
        }

        //Login /api/Auth/Login POST
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password); 

                if (checkPasswordResult)
                {
                    //  Create token
                    return Ok();
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
