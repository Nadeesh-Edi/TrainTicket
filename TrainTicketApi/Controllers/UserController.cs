/******************************************************************************
 * UserController.cs
 * 
 * Description: This file contains the implementation of a controller for managing users.
 * The users includes Base User and Travel Agent
 * Role - 1 Base USer
 * Role - 2 Travel Agent
 * 
 * 
 *****************************************************************************/


using TrainTicketApi.Services;
using TrainTicketApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrainTicketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        // Get all Users
        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        // Get Users by Id
        [HttpGet("get")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        // Register a User
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }

        // User login API which authenticates and returns the role
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest body)
        {
            if (body == null)
            {
                return BadRequest("Invalid request");
            }

            List<User> users = await Get();
            IActionResult result = null;

            foreach (var user in users)
            {
                if (user.Email == body.username)
                {
                    if (user.Pwrd == body.password)
                    {
                        var results = new
                        {
                            user.Role,
                            user.Status
                        };
                        result = Ok(results);
                        break;
                    }
                    else
                    {
                        result = Content("Incorrect password");
                    }
                }
            }

            if (result == null) 
            {
                result = Content("User not found");
            }

            return result;
        }

        // Deactivate User
        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            User user = await _userService.GetAsync(id);

            if (user == null)
            {
                return Content("Invalid user");
            }

            if (user.Status == 0)
            {
                return Content("User is already deactivated");
            } 
            else
            {
                user.Status = 0;
                await _userService.UpdateAsync(id, user);
                return Ok(user);
            }
        }

        // Reactivate User
        [HttpPost("reactivate")]
        public async Task<IActionResult> ReActivateUser(string id)
        {
            User user = await _userService.GetAsync(id);

            if (user == null)
            {
                return Content("Invalid user");
            }

            if (user.Status == 1)
            {
                return Content("User is already activated");
            }
            else
            {
                user.Status = 1;
                await _userService.UpdateAsync(id, user);
                return Ok(user);
            }
        }
    }
}
