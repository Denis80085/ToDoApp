using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ToDoDtos;
using api.models;

namespace api.Interfaces
{
    public interface IToDoRepository
    {
        Task<List<ToDoModel>> GetAllToDo();

        Task<ToDoModel> CreateToDo(CreateToDoDto createToDo);
    }
}