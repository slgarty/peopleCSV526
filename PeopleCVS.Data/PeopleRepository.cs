using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace PeopleCVS.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string GeneratePeopleCSV(int amount)
        {
            var ppl = new List<Person>();
            for (int i = 1; i <= amount; i++)
            {
                var p = new Person()
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Address = Faker.Address.StreetAddress(),
                    Age = Faker.RandomNumber.Next(0, 90),
                    Email = Faker.Internet.FreeEmail(),
                };
                ppl.Add(p);
            }
            string csv=  GetCsv(ppl);
            return csv;
          
        }

            public void AddList(byte[] csvBytes)
        {
            var people = GetFromCsv(csvBytes);
            using var context = new PeopleDbContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }
        public List<Person> GetAll()
        {
            using var context = new PeopleDbContext(_connectionString);
            
            var ppl= context.People;
            if(ppl==null)
            {
                return null;
                    }
            return ppl.ToList();
        }
        public void DeleteAll()
        {
            using var context = new PeopleDbContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM People");
        }

        public string GetCsv(List<Person> ppl)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);

            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(ppl);

            return builder.ToString();
        }

        public List<Person> GetFromCsv(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Person>().ToList();
        }
    }
}
