using Application.Contracts;
using Application.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        public UserController(IUser user)
        {
            this.user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LogUserIn(LoginUserDTO loginDTO)
        {
            var result = await user.LoginUserAsync(loginDTO);

            return Ok(result);
   
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterUser(RegisterUserDTO registerDTO)
        {
            var result = await user.RegisterUserAsync(registerDTO);

            return Ok(result);

        }
    }
}
