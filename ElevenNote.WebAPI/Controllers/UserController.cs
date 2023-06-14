using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevenNote.Models.User;
using ElevenNote.Services.Token;
using ElevenNote.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNote.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        //1.method support, 2.route
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _userService.RegisterUserAsync(model);
            if (registerResult)
            {
                return Ok("User was registered.");
            }
            else
            {
                return BadRequest("User could not be registered.");
            }
        }
        [Authorize]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            var UserDetail = await _userService.GetUserByIdAsync(userId);
            if (UserDetail is null) //"==" same as "is"
            {
                return NotFound();
            }
            return Ok(UserDetail);
        }
    }
}
