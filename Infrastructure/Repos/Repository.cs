using Microsoft.EntityFrameworkCore;
using SQL_API.Infrastructure;
using SQL_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Infrastructure.Repos
{
    public class Repository: IRepository
    {
        private readonly SqlContext _context;

        public Repository(SqlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() =>
           await _context.Users
            .OrderBy(u => u.Id)
            .ToArrayAsync();

        public async Task<User> GetUserAsync(int id) =>        
           await _context.Users.FindAsync(id);

        public async Task<IEnumerable<User>> GetByCompanyAsync(int companyId) =>
           await  _context.Users.Where(u => u.CompanyId == companyId).ToListAsync();

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {           
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            var u = GetUserAsync(id).Result;
            if (u != null)
            {
                var state = _context.Users.Remove(u).State.ToString();
                await _context.SaveChangesAsync();
                return state;
            }
            return null;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync() =>
           await _context.Companies
            .OrderBy(u => u.Id)
            .ToArrayAsync();

        public async Task<Company> GetCompanyAsync(int id) =>
           await _context.Companies.FindAsync(id);

        public async Task AddCompanyAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task<string> DeleteCompanyAsync(int id)
        {
            var c = GetCompanyAsync(id).Result;
            if (c!= null)
            {
                var state = _context.Companies.Remove(c).State.ToString();
                await _context.SaveChangesAsync();
                return state;
            }
            return null;
        }

    }
}
