using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApp1.Controllers
{

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>()
        {
            new User(){ FirstName = "John", LastName = "Doe", Age = 22},
            new User(){ FirstName = "Jane", LastName = "Deal", Age = 16},
            new User(){ FirstName = "Shawn", LastName = "Pawn", Age = 44},
            new User(){ FirstName = "Mike", LastName = "Trix", Age = 17}
        };
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return users;
        }
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                return users[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound($"User with id - {id}, does not exist! Try again!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }
        [HttpGet("age/{id}")]
        public ActionResult<User> CheckAdult(int id)
        {
            try
            {
                if(users[id-1].Age >= 18)
                {
                    return Ok($"The user with name {users[id - 1].FirstName} {users[id - 1].LastName} and id {id} is adult!");
                }
                else
                {
                    return Ok($"The user with name {users[id - 1].FirstName} {users[id - 1].LastName} and id {id} is not an adult!");
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound($"User with id - {id}, does not exist! Try again!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}. Try again!");
            }
        }
        [HttpPost]
        public IActionResult Post()
        {
            string body;
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                body = sr.ReadToEnd();
            }
            User user = JsonConvert.DeserializeObject<User>(body);
            users.Add(user);
            return Ok($"User with id {users.Count} has been added to the list!");
        }
    }
}