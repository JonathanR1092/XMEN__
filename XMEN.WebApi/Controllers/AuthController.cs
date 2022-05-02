using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XMEN.Services.Interfaces;
using XMEN.WebApi.Configuration;
using XMEN.WebApi.DTOs;

namespace XMEN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService _service;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto registerUser)
        {
            if (ModelState.IsValid)
            {
                var existringUser = await _userManager.FindByEmailAsync(registerUser.Email);

                if (existringUser != null)
                {
                    return BadRequest("El correo electrónico ya existe!");
                }

                var isCreated = await _userManager.CreateAsync(new IdentityUser() { Email = registerUser.Email, UserName = registerUser.Email }, registerUser.Password);

                if (isCreated.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }
            }
            else
            {
                return BadRequest("Error al registrar el usuario");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    return BadRequest(new UserLoginResponseDto()
                    {
                        Login = false,
                        Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (isCorrect)
                {
                    var tokenParameters = new TokenParameters()
                    {
                        Id = existingUser.Id,
                        PasswordHash = existingUser.PasswordHash,
                        UserName = existingUser.UserName
                    };

                    var jwtToken = _service.GenerateJwtToken(tokenParameters);

                    return Ok(new UserLoginResponseDto()
                    {
                        Login = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new UserLoginResponseDto()
                    {
                        Login = false,
                        Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto"
                        }
                    });
                }
            }
            else
            {
                return BadRequest(new UserLoginResponseDto()
                {
                    Login = false,
                    Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto"
                        }
                });
            }
        }
    }
}