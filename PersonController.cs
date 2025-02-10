using BasicDapperData.Data.Models;
using BasicDapperData.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasicDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _personRepository;
        public PersonController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await _personRepository.GetPeople();
            return Ok(people);
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null)
                return NotFound();
            
            return Ok(person);
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            if(!ModelState.IsValid)
                return BadRequest("Invalid data");

            var result = await _personRepository.AddPerson(person);

            if (!result)
                return BadRequest("Could not sava data");

            return Ok();
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person newPerson)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var person = _personRepository.GetPersonById(id);
            if (person == null)
                return NotFound("There is no person with that id");
            newPerson.Id = id;
            var result = await _personRepository.UpdatePerson(newPerson);

            if (!result)
                return BadRequest("Could not sava data");

            return Ok();
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = _personRepository.GetPersonById(id);
            if (person == null)
                return NotFound("There is no person with that id");

            var result = await _personRepository.DeletePerson(id);

            if (!result)
                return BadRequest("Could not sava data");

            return Ok();
        }
    }
}
