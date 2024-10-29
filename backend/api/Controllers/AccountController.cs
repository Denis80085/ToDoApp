using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.UserAccount;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _UserManager;

        private readonly ITokenService _TokenService;

        public AccountController(UserManager<AppUser> UserManager, ITokenService TokenService)
        {
            _UserManager = UserManager;
            _TokenService = TokenService;
        }

        [HttpPost("test")]
        public IActionResult TestingTokenCreate()
        {
            var user = new AppUser
            {
                UserName = "ItsMeMario",
                Email = "mario666@ggg.com"
            };

            var token = _TokenService.CreateToken(user);

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _UserManager.CreateAsync(appUser, registerDto.Password);

                if(createdUser.Succeeded)
                {
                    var RoleResult = await _UserManager.AddToRoleAsync(appUser, "user");

                    if(RoleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _TokenService.CreateToken(appUser)
                        });

                    }
                    else
                    {
                        return StatusCode(500, RoleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
           
        }
    }
}