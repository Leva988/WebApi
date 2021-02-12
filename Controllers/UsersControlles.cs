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
            return Ok(user.Result);
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

        //GET item
        [HttpGet("Photo/{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await repository.GetUserPhotoAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost("{id}/Photo")]
        public async Task<ActionResult> PostPhoto(int id, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var stream = file.OpenReadStream();
            var memory = new MemoryStream();
            stream.CopyTo(memory);
            var bytes = memory.ToArray();
            await repository.AddUserPhotoAsync(id, bytes, file.ContentType);
            return CreatedAtAction(nameof(GetItem), new { id = id, }, new { photoId = id, });
        }

        [HttpDelete("{id}/Photo")]
        public ActionResult DeletePhoto(int id)
        {
            var resp = repository.DeletePhotoAsync(id).Result;
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
