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
                Debug.WriteLine("!!!!!!!!!Warnind Exception" + e.ToString());
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
                Debug.WriteLine("!!!!!!!!!Warnind Exception" + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id:int}")]
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
                    return Ok(ToDoModel);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!!!!!Warnind Exception" + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateToDoDto updateDTO, [FromQuery] ToDoQuerryObject querryObject)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                
                var UpdatedToDo = await _ToDoRepo.UpdateToDo(id, updateDTO, querryObject);

                if(UpdatedToDo != null)
                {
                    return Ok(UpdatedToDo);
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