// Controller for Train Model

using TrainTicketApi.Services;
using TrainTicketApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrainTicketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _scheduleService;
        private readonly TrainService _trainService;

        public ScheduleController(ScheduleService scheduleService, TrainService trainService)
        {
            _scheduleService = scheduleService;
            _trainService = trainService;
        }

        // Get all schedules from db
        [HttpGet]
        public async Task<List<Schedule>> Get() =>
            await _scheduleService.GetAsync();

        // Get a schedule by id
        [HttpGet("get")]
        public async Task<ActionResult<Schedule>> Get(string id)
        {
            var schedule = await _scheduleService.GetAsync(id);

            if (schedule is null)
            {
                return NotFound();
            }
            return schedule;
        }

        // Get schedules by train Id
        [HttpGet("getByTrain")]
        public async Task<List<Schedule>> GetByTrain(string id) =>
            await _scheduleService.GetAsyncByTrain(id);

        // Create new schedule
        [HttpPost("create")]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            Train selectedTrain;
            try
            {
               selectedTrain  = await _trainService.GetAsync(schedule.TrainId);
            }
            catch (Exception ex)
            {
                return NotFound("Invalid Train Id");
            }
            
            if (selectedTrain is null)
            {
                return NotFound("Invalid Train Id");
            }

            await _scheduleService.CreateAsync(schedule);

            return CreatedAtAction(nameof(Create), new { id = schedule.Id }, schedule);
        }

        // Edit schedule details
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(string id, Schedule schedule)
        {
            Schedule schedule1 = await _scheduleService.GetAsync(id);

            if (schedule1 == null)
            {
                return NotFound();
            }

            schedule.Id = id;
            await _scheduleService.UpdateAsync(id, schedule);
            return Ok(schedule);
        }
    }
}
