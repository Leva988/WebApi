using Microsoft.AspNetCore.Mvc;
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

    public class CompaniesController : Controller
    {
        private readonly IRepository repository;

        public CompaniesController(IRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var companies = await repository.GetCompaniesAsync();
            foreach (Company company in companies)
            {
                var users = await repository.GetByCompanyAsync(company.Id);
                company.Users = users;
            }
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetbyID(int id)
        {
            var company = await repository.GetCompanyAsync(id);
            if (company == null)
            {

                return NotFound();
            }
            var users = await repository.GetByCompanyAsync(company.Id);
            company.Users = users;
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CompanyNew comNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var company = MapCompany(comNew);
            await repository.AddCompanyAsync(company);
            return CreatedAtAction(nameof(GetbyID), new { id = company.Id }, new { id = company.Id });
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CompanyNew comNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var company = MapCompany(comNew);
            await repository.UpdateCompanyAsync(company);
            return CreatedAtAction(nameof(GetbyID), new { id = company.Id }, new { id = company.Id });
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var resp = repository.DeleteCompanyAsync(id).Result;
            if (resp == null)
            {
                return NotFound();
            }
            return Ok(resp);
        }

        private Company MapCompany(CompanyNew comNew) =>
            new Company
            {
                Name = comNew.Name                
            };
    }

}
