using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SQL_API.Infrastructure;
using SQL_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Infrastructure.Repos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

       private readonly IRepository repository;

       public UsersController(IRepository repository)
        {
            this.repository = repository;
        } 

       
       [HttpGet] 
       public async Task<ActionResult> GetAll()
        {
           var users = await repository.GetUsersAsync();
           return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetbyID(int id)
        {
            var user = await repository.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("Company/{companyId}")]
        public async Task<ActionResult> GetbyCompanyId(int companyId)
        {
            var users = await repository.GetByCompanyAsync(companyId);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserNew userNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var user = MapUser(userNew);
            await repository.AddUserAsync(user);
            return  CreatedAtAction(nameof(GetbyID), new { id = user.Id }, new { id = user.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserNew userNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var user = MapUser(userNew);
            user.Id = id;
            await repository.UpdateUserAsync(user);
            return  CreatedAtAction(nameof(GetbyID), new { id = user.Id }, new { id = user.Id });
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var resp = await repository.DeleteUserAsync(id);
            if (resp == null)
            {
                return NotFound();
            }
            return Ok(resp);
        }

        private User MapUser(UserNew userNew) =>
        new User
        {
            Name = userNew.Name,
            Activity = userNew.Activity,
            CompanyId = userNew.CompanyId
        };
    }
}
