using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ToDoDtos;
using api.Extentions;
using api.Helpers;
using api.Interfaces;
using api.Mappers.ToDoMapers;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private IToDoRepository _ToDoRepo;
        private UserManager<AppUser> _userManager;
        private IPortfolioRepository _PortfolioRepo;

        public ToDoController(IToDoRepository ToDoRepo, UserManager<AppUser> userManager, IPortfolioRepository PortfolioRepo)
        {
            _ToDoRepo = ToDoRepo;
            _userManager = userManager;
            _PortfolioRepo = PortfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ToDos = await _ToDoRepo.GetAllToDo();

                return Ok(ToDos.Select(d => d.MapToDoDto()).ToList());
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!!!!!Warnind Exception" + e.ToString());
                return StatusCode(500, e.ToString());
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateToDoDto createToDo, [FromQuery] ToDoQuerryObject querryObject)
        {
            try
            {
                if(!ModelState.IsValid) 
                    return BadRequest(ModelState);

                string? UserName = User.GetUserName();

                if(UserName == null)
                {
                    return NotFound("User could not be found");
                }

                AppUser? appUser = await _userManager.FindByNameAsync(UserName);

                if(appUser == null)
                {
                    return NotFound("User could not be found");
                }

                var ToDo = await _ToDoRepo.CreateToDo(createToDo, querryObject);

                if(ToDo != null)
                {
                    var portfolio  = await _PortfolioRepo.CreatePortfolio(ToDo.Id, appUser);

                    if(portfolio == null)
                    {
                        return StatusCode(500, "Your ToDo could not be added to portfolio");
                    }

                    return Ok(ToDo.MapToDoDto());
                }
                else
                {
                    return StatusCode(500, "ToDo could not be created");
                }                       
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!!!!!Warnind Exception" + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if(!ModelState.IsValid) 
                    return BadRequest(ModelState);

                var ToDoModel = await _ToDoRepo.DeleteToDo(id);

                if(ToDoModel == null)
                {
                    return BadRequest($"ToDo with id = {id} could not be found");
                }
                else
                {
                    return Ok(ToDoModel.MapToDoDto());
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!!!!!Warnind Exception" + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateToDoDto updateDTO, [FromQuery] ToDoQuerryObject querryObject)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                
                var UpdatedToDo = await _ToDoRepo.UpdateToDo(id, updateDTO, querryObject);

                if(UpdatedToDo != null)
                {
                    return Ok(UpdatedToDo.MapToDoDto());
                }
                else
                {
                    return BadRequest("The to do could not be updated");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!!!!!Warnind Exception" + e.Message);
                return StatusCode(500, e);
            }
        }
    }
}