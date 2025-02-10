using BasicDapperData.Data;
using BasicDapperData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDapperData.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDataAccess _dbAccess;
        public PersonRepository(IDataAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                string query = "INSERT INTO dbo.Person(Name,Email) VALUES(@Name,Email)";
                await _dbAccess.SaveDataAsync(query, new { Name = person.Name, Email = person.Email });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                string query = "DELETE FROM dbo.Person WHERE Id=@Id";
                await _dbAccess.SaveDataAsync(query, new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            string query = "SELECT * FROM dbo.Person";
            var people = await _dbAccess.GetDataAsync<Person, dynamic>(query, new { });
            return people;
        }

        public async Task<Person> GetPersonById(int id)
        {
            string query = "SELECT * FROM dbo.Person WHERE Id=@Id";
            var person = await _dbAccess.GetDataAsync<Person, dynamic>(query, new { Id = id});
            return person.FirstOrDefault();
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                string query = "UPDATE dbo.Person SET Name=@Name, Email=@Email WHERE Id=@Id";
                await _dbAccess.SaveDataAsync(query, new { Name = person.Name, Email = person.Email, Id = person.Id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
