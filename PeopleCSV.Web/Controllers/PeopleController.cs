using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PeopleCVS.Data;
using System.Collections.Generic;
using System.Text;
using PeopleCSV.Web.ViewModels;
using System;

namespace PeopleCSV.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly string _connectionString;
    
        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("GetAll")]
        public List<Person> GetAll()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetAll();
        }
        [HttpGet]
        [Route("generatePeople")]
        public IActionResult generatePeople(int amount)
        {
            var repo = new PeopleRepository(_connectionString);
            var csv = repo.GeneratePeopleCSV(amount);
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "people.csv");

        }
       
        [HttpPost]
        [Route("deleteAll")]
        public void DeleteAll()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }
        [HttpPost]
        [Route("upload")]
        public void Upload(string Base64File)
        {

            int commaIndex = Base64File.IndexOf(',');
            string base64 = Base64File.Substring(commaIndex + 1);
            var file = Convert.FromBase64String(base64);
            var repo = new PeopleRepository(_connectionString);
            repo.AddList(file);
        }

    }
}
