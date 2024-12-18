using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ToDoDtos;
using api.Helpers;
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

        public async Task<ToDoModel?> CreateToDo(CreateToDoDto createToDo, ToDoQuerryObject querryObject)
        {
            ToDoModel ToDo = new ToDoModel
            {
                Titel = createToDo.Titel,
                Task = createToDo.Task,
                State = createToDo.State
            };

            if(querryObject.WithDeadline)
            {
                if(querryObject.ToDate != null & querryObject.FromDate !=  null)
                {
                    ToDo.FromDate = querryObject.FromDate;
                    ToDo.ToDate = querryObject.ToDate;
                }
            }

            if(ToDo.FromDate > ToDo.ToDate) return null;

            await _context.ToDos.AddAsync(ToDo);
            await _context.SaveChangesAsync();

            return ToDo;
        }

        public async Task<ToDoModel?> DeleteToDo(int id)
        {
            var ToDoModel = await _context.ToDos.FirstOrDefaultAsync(d => d.Id == id);

            if(ToDoModel != null)
            {
                _context.ToDos.Remove(ToDoModel);
                
                await _context.SaveChangesAsync();
            }

            return ToDoModel;
        }

        public async Task<List<ToDoModel>> GetAllToDo()
        {
            return await _context.ToDos.Select(d => d).ToListAsync();
        }

        public async Task<ToDoModel?> UpdateToDo(int id, UpdateToDoDto UpdateDTO, ToDoQuerryObject querryObject)
        {
            try
            {
                var ToDo = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id);

                if(ToDo == null)
                {
                    return null;
                }
                else
                {
                    ToDo.Titel = UpdateDTO.Titel;
                    ToDo.Task = UpdateDTO.Task;
                    ToDo.State = UpdateDTO.State;

                    if(querryObject.WithDeadline)
                    {
                        if(querryObject.FromDate != null & querryObject.ToDate != null)
                        {
                            ToDo.FromDate = querryObject.FromDate;
                            ToDo.ToDate = querryObject.ToDate;
                        }

                    }
                    
                    await _context.SaveChangesAsync();

                    return ToDo;
                }
            }
            catch(DbUpdateException ue)
            {
                Debug.WriteLine("!!!!!!!!!Warning DbUpdateException" + ue.Message);
                return null;
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!!!!!Warnind Exception" +  e.Message);
                return null;
            }
        }
    }
}