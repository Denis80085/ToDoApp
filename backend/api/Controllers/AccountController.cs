using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.UserAccount;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _UserManager;

        private readonly ITokenService _TokenService;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> UserManager, ITokenService TokenService, SignInManager<AppUser> signInManager)
        {
            _UserManager = UserManager;
            _TokenService = TokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _UserManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.UserNameOrEmail);

                user ??= await _UserManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserNameOrEmail);

                if(user != null)
                {
                    var result =  await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                    if(result.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _TokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        return Unauthorized("Login failed. Wrong Password and/or Username or Email.");
                    }
                }
                else
                {
                    return Unauthorized("Login failed. Wrong Password and/or Username or Email.");
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
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