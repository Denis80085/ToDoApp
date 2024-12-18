using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Portfolio?> CreatePortfolio(int ToDoId, AppUser User);

        Task<List<ToDoModel?>> GetPortfolios(AppUser User);

        Task<bool> UserHasToDoId(int id, AppUser User);
    }
}