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
        //[HttpGet("getByTrain")]
        //public async Task<List<Schedule>> GetByTrain(string id) =>
        //    await _scheduleService.GetAsyncByTrain(id);

        // Create new schedule
        [HttpPost("create")]
        public async Task<IActionResult> Create(Schedule schedule)
        {
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

        // Filter schedule
        [HttpGet("filter")]
        public async Task<List<Schedule>> Filter(string start, string end, string date)
        {
            Station starting = new Station();
            starting.name = start;
            List<Schedule> availableSchedules = new List<Schedule>();

            Station ending = new Station();
            ending.name = end;

            List<Schedule> schedules = new List<Schedule>();

            var allSchedules = await _scheduleService.GetAsync();

            foreach (var x in allSchedules)
            {
                if ((x.Date.Day.ToString() + '-' + x.Date.Month + '-' + x.Date.Year) == date)
                    availableSchedules.Add(x);
            }

            foreach (var item in availableSchedules)
            {
                List<Station> stations = item.StationList;
                if (stations.Contains(starting) && stations.Contains(ending)) { schedules.Add(item); }
            }

            return schedules;
        }
    }
}
