using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Students;
using DemoApp.API.Models.Dtos.Authes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult =  await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult= await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded) return Ok("Created User Succesfully!");
                }
                
            }
            return BadRequest("Something went wrong :(");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if(user != null)
            {
                var checkPassWordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPassWordResult)
                {
                    // Get roles

                    var roles = await userManager.GetRolesAsync(user);

                    if(roles!= null)
                    {
                        // Create token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        return Ok(new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        });
                    }

                    return BadRequest("User role is empty or wrong now, please check again");

                }
                return BadRequest("Your password is wrong, please check again");
            }
            return BadRequest("Can not find your account");
        }


    }
}
