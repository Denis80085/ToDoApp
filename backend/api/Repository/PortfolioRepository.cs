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
    }
}