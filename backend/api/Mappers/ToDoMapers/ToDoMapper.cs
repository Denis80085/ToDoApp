using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ToDoDtos;
using api.models;

namespace api.Mappers.ToDoMapers
{
    public static class ToDoMapper
    {
        public static ToDoDto? MapToDoDto(this ToDoModel? toDoModel)
        {
            if(toDoModel == null)
            {
                return null;
            }
            else
            {
                return new ToDoDto
                {
                    Id = toDoModel.Id,
                    Titel = toDoModel.Titel,
                    Task = toDoModel.Task,
                    CreatedOn = toDoModel.CreatedOn,
                    State = toDoModel.State,
                    FromDate = toDoModel.FromDate,
                    ToDate = toDoModel.ToDate
                };
            }
            
        }
    }
}