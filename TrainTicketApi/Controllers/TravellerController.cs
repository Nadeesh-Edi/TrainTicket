using TrainTicketApi.Services;
using TrainTicketApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrainTicketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravellerController : ControllerBase
    {
        private readonly TravellerService _travellerService;

        public TravellerController(TravellerService travellerService) =>
            _travellerService = travellerService;

        [HttpGet]
        public async Task<List<Traveller>> Get() =>
            await _travellerService.GetAsync();

        [HttpGet("get")]
        public async Task<ActionResult<Traveller>> Get(string id)
        {
            var user = await _travellerService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Traveller user)
        {
            await _travellerService.CreateAsync(user);

            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest body)
        {
            if (body == null)
            {
                return BadRequest("Invalid request");
            }

            List<Traveller> users = await Get();
            IActionResult result = null;

            foreach (var user in users)
            {
                if (user.Nic == body.username)
                {
                    if (user.Pwrd == body.password)
                    {
                        // JsonResult results = new JsonResult(user.Role, user.Status);
                        if (user.Status == 0)
                        {
                            result = Content("You are deactivated. Please contact admin");
                        }
                        else
                        {
                            result = Ok();
                        }
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

        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            Traveller user = await _travellerService.GetAsync(id);

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
                await _travellerService.UpdateAsync(id, user);
                return Ok(user);
            }
        }

        [HttpPost("reactivate")]
        public async Task<IActionResult> ReActivateUser(string id)
        {
            Traveller user = await _travellerService.GetAsync(id);

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
                await _travellerService.UpdateAsync(id, user);
                return Ok(user);
            }
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(string id, Traveller traveller)
        {
            Traveller traveller1 = await _travellerService.GetAsync(id);

            if (traveller1 == null)
            {
                return NotFound();
            }

            traveller.Id = id;
            await _travellerService.UpdateAsync(id, traveller);
            return Ok(traveller);
        }
    }
}
