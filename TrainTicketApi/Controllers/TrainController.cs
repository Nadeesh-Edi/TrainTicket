/******************************************************************************
* TrainController.cs
* 
* Description: This file contains the implementation of a controller for managing trains.
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
    public class TrainController : ControllerBase
    {
        private readonly TrainService _trainService;

        public TrainController(TrainService trainService) =>
            _trainService = trainService;

        // Get all trains from db
        [HttpGet]
        public async Task<List<Train>> Get() =>
            await _trainService.GetAsync();

        // Get a train by id
        [HttpGet("get")]
        public async Task<ActionResult<Train>> Get(string id)
        {
            var train = await _trainService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            return train;
        }

        // Create new train
        [HttpPost("create")]
        public async Task<IActionResult> Create(Train train)
        {
            await _trainService.CreateAsync(train);

            return CreatedAtAction(nameof(Create), new { id = train.Id }, train);
        }

        // Edit train details
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(string id, Train train)
        {
            Train train1 = await _trainService.GetAsync(id);

            if (train1 == null)
            {
                return NotFound();
            }

            train.Id = id;
            await _trainService.UpdateAsync(id, train);
            return Ok(train);
        }
    }
}
