using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ToDoDtos;
using api.Helpers;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private IToDoRepository _ToDoRepo;
        public ToDoController(IToDoRepository ToDoRepo)
        {
            _ToDoRepo = ToDoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ToDos = await _ToDoRepo.GetAllToDo();

                return Ok(ToDos);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateToDoDto createToDo, [FromQuery] ToDoQuerryObject querryObject)
        {
            try
            {
                //To Do: make sure that to date is grather than from date
                if(!ModelState.IsValid) 
                    return BadRequest(ModelState);

                var ToDo = await _ToDoRepo.CreateToDo(createToDo, querryObject);

                if(ToDo == null) 
                    return StatusCode(500, "ToDo could not be created");

                return Ok(ToDo);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}