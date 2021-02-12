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
                var state = _context.Users.Remove(u).ToString();
                await _context.SaveChangesAsync();
                return state;
            }
            return null;
        }

        public async Task<FileStreamResult> GetUserPhotoAsync(int id)
        {
            var userPhoto = await _context.UserPhotos.FindAsync(id);
            if(userPhoto!=null)
            {
                var stream = new MemoryStream(userPhoto.Photo);
                return new FileStreamResult(stream, userPhoto.ContentType);
            }
            return null;           
        }


        public async Task AddUserPhotoAsync(int userId, byte[] input, string contentType)
        {
            var userPhoto = new UserPhoto()
            {
                Id = userId,
                Photo = input,
                ContentType = contentType
            };
            await _context.AddAsync(userPhoto);
            await _context.SaveChangesAsync();             
        }

        public async Task<string> DeletePhotoAsync(int id)
        {
            var u = await _context.UserPhotos.FindAsync(id);
            if (u != null)
            {
                // SqlParameter photoId = new SqlParameter("@photoId", id);
                // var del = await  _context.Database.ExecuteSqlRawAsync("Delete From UserPhotos where Id = @photoId", photoId);
                //  var numberofRows = del.ToString();
                var state = _context.UserPhotos.Remove(u).ToString();
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
