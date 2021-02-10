using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SQL_API.Infrastructure;
using SQL_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Infrastructure.Repos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public ActionResult GetbyID(int id)
        {
            var user = repository.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("Company/{companyId}")]
        public async Task<ActionResult> GetbyCompanyID(int companyId)
        {
            var users = await repository.GetByCompanyAsync(companyId);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            await repository.AddUserAsync(user);
            return  CreatedAtAction(nameof(GetbyID), new { id = user.Id }, new { id = user.Id });
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            await repository.UpdateUserAsync(user);
            return  CreatedAtAction(nameof(GetbyID), new { id = user.Id }, new { id = user.Id });
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var resp = repository.DeleteUserAsync(id).Result;
            if (resp == null)
            {
                return NotFound();
            }
            return Ok(resp);
        }

    }
}
