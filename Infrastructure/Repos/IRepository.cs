using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQL_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Infrastructure.Repos
{
    public interface IRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserAsync(int id);

        Task<IEnumerable<User>> GetByCompanyAsync(int companyId);

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task<string> DeleteUserAsync(int id);

        Task<FileStreamResult> GetUserPhotoAsync(int id);

        Task AddUserPhotoAsync(int userId, byte[] input, string contentType);

        Task<string> DeletePhotoAsync(int id);

        Task<IEnumerable<Company>> GetCompaniesAsync();

        Task<Company> GetCompanyAsync(int id);

        Task AddCompanyAsync(Company company);

        Task UpdateCompanyAsync(Company company);

        Task<string> DeleteCompanyAsync(int id);
    }
}
