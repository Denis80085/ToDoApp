using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ToDoDtos;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private AplicationDBContext _context;

        public ToDoRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ToDoModel> CreateToDo(CreateToDoDto createToDo)
        {
            ToDoModel ToDo = new ToDoModel
            {
                Titel = createToDo.Titel,
                Task = createToDo.Task,
                State = createToDo.State
            };

            await _context.ToDos.AddAsync(ToDo);
            await _context.SaveChangesAsync();

            return ToDo;
        }

        public async Task<List<ToDoModel>> GetAllToDo()
        {
            return await _context.ToDos.Select(d => d).ToListAsync();
        }
    }
}