using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SQL_API.Infrastructure;
using SQL_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Infrastructure.Repos
{
    public class Repository: IRepository
    {
        private readonly SqlContext _context;

        public Repository(SqlContext context)
        {
            _context = context;
        }

        #region User
        public async Task<IEnumerable<UserWithCompany>> GetUsersAsync() =>
           await _context.Users.Join(_context.Companies,
            u => u.CompanyId,
            c => c.Id,
            (u, c) => new UserWithCompany() {
                Id = u.Id,
                Name = u.Name,
                Age= u.Age,
                Activity = u.Activity,
                Company = c.Name
            })
            .AsNoTracking()                       
            .OrderBy(u => u.Id)
            .ToArrayAsync();

        public async Task<UserWithCompany> GetUserAsync(int id) =>        
           await _context.Users.Join(_context.Companies,
            u => u.CompanyId,
            c => c.Id,
            (u, c) => new UserWithCompany() {
                Id = u.Id,
                Name = u.Name,
                Age= u.Age,
                Activity = u.Activity,
                Company = c.Name
            })
           .AsNoTracking()                    
           .Where(u => u.Id == id)
           .FirstAsync();

        public async Task<IEnumerable<UserWithCompany>> GetByCompanyAsync(int companyId) =>
           await  _context.Users                               
             .Where(u => u.CompanyId == companyId)
             .Join(_context.Companies,
              u => u.CompanyId,
              c => c.Id,
              (u, c) => new UserWithCompany() {
                  Id = u.Id,
                  Name = u.Name,
                  Age= u.Age,
                  Activity = u.Activity,
                  Company = c.Name
              })
            .AsNoTracking()
            .ToListAsync();

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
            var u = await _context.Users.FindAsync(id);
            if (u != null)
            {
                var state = _context.Users.Remove(u).State.ToString();
                await _context.SaveChangesAsync();
                return state;
            }
            return null;
        }
        #endregion

        #region Company
        public async Task<IEnumerable<Company>> GetCompaniesAsync() =>
           await _context.Companies
            .OrderBy(u => u.Id)
            .AsNoTracking()
            .ToListAsync();

        public async Task<Company> GetCompanyAsync(int id) =>
           await _context.Companies
           .FindAsync(id);

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
            var c = await GetCompanyAsync(id);
            if (c!= null)
            {
                var state = _context.Companies.Remove(c).State.ToString();
                await _context.SaveChangesAsync();
                return state;
            }
            return null;
        }
        #endregion
    }
}
