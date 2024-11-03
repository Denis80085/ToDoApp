using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Extentions;
using api.Interfaces;
using api.Mappers.ToDoMapers;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _portfolioRepo = portfolioRepo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string? UserName = User.GetUserName();
                
                if(UserName != null)
                {
                    var user = await _userManager.FindByNameAsync(UserName);

                    if(user == null)
                    {
                        return Unauthorized("User not found");
                    }

                    var UserToDos = await _portfolioRepo.GetPortfolios(user);


                    return Ok(UserToDos.Select(x => x.MapToDoDto()).ToList());
                }
                else
                {
                    return Unauthorized("User not found");
                }
                
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!Exception Portfolio:" + e);
                return StatusCode(500, e.Message);
            }
        }
    }
}