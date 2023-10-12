﻿/******************************************************************************
* ScheduleController.cs
* 
* Description: This file contains the implementation of a controller for managing train schedules.
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
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _scheduleService;
        private readonly TrainService _trainService;
        private readonly ReservationService _reservationService;

        public ScheduleController(ScheduleService scheduleService, TrainService trainService, ReservationService reservationService)
        {
            _scheduleService = scheduleService;
            _trainService = trainService;
            _reservationService = reservationService;
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

            var allSchedules = await _scheduleService.GetActiveAsync();

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

        // Delete traveller
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var reservationsForSchedule = await _reservationService.GetAsyncBySchedule(id);

            try
            {
                await _scheduleService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
