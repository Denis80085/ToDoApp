using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private AplicationDBContext _context;

        public PortfolioRepository(AplicationDBContext  context)
        {
            _context = context;
        }

        public async Task<Portfolio?> CreatePortfolio(int ToDoId, AppUser User)
        {
            try
            {
                Portfolio? NewPortfolio = new Portfolio{
                ToDoId = ToDoId,
                UserId = User.Id};

                var portfolio = await _context.Portfolios.AddAsync(NewPortfolio);

                if(portfolio != null)
                {
                    await _context.SaveChangesAsync();
                    return NewPortfolio;
                }
                else
                {
                    return null;
                }
            }
            catch(SqlException sqle)
            {
                Debug.WriteLine("!!!!!SQLEXCEPTION" + sqle.Message);
                return null;
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!!!!Error" + e.Message);
                return null;
            }
            
        }

        public async Task<List<ToDoModel?>> GetPortfolios(AppUser User)
        {
            return await _context.Portfolios.Where(p => p.UserId == User.Id).Select(p => p.ToDo).ToListAsync();
        }

        public async Task<bool> UserHasToDoId(int id, AppUser User)
        {
            var UserToDos = await GetPortfolios(User);
            
            if(UserToDos == null)
            {
                return false;
            }

            return UserToDos.Any(d => d.Id == id);
            
        }
    }
}