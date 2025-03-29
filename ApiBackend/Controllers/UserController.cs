
using ApiBackend.Services;
using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUsers()
        {
            var users = await _userService.getUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound("Usuario no encontrado");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("Datos de usuario vienen vacios");
            }
            var newUser = await _userService.CreateUser(user);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("Datos de usuario vienen vacios");
            }

            var response = await _userService.UpdateUser(id, updatedUser);

            if (response == false)
            {
                return NotFound("Usuario no encontrado en base de datos");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUSer(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response == false)
            {
                return NotFound("Usuario no encontrado en base de datos");
            }
            return NoContent();
        }
    }
}
