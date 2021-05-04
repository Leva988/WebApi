using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Infrastructure.Repos
{
    public interface IRepository
    {
        #region  User
        Task<IEnumerable<UserWithCompany>> GetUsersAsync();

        Task<UserWithCompany> GetUserAsync(int id);

        Task<IEnumerable<UserWithCompany>> GetByCompanyAsync(int companyId);

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task<string> DeleteUserAsync(int id);
        #endregion

        #region Company
        Task<IEnumerable<Company>> GetCompaniesAsync();

        Task<Company> GetCompanyAsync(int id);

        Task AddCompanyAsync(Company company);

        Task UpdateCompanyAsync(Company company);

        Task<string> DeleteCompanyAsync(int id);
        #endregion
    }
}
