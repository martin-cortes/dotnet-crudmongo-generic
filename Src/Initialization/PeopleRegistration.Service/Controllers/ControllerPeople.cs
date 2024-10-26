using Application.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using PeopleRegistration.Service.Controllers.Base;

namespace PeopleRegistration.Service.Controllers
{
    [Produces("Application/json")]
    [Route("api/[controller]/[action]")]
    public class ControllerPeople : BaseController
    {
        private readonly ILogger<ControllerPeople> _logger;
        private readonly IPeopleService _peopleService;

        public ControllerPeople(ILogger<ControllerPeople> logger, IPeopleService peopleService) : base(logger)
        {
            _logger = logger;
            _peopleService = peopleService;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPeopleById(string id)
        {
            _logger.LogInformation("Get by id -- Id: {Id}", id);

            return await HandleResponse(async () =>
            {
                return await _peopleService.GetByIdAsync(id);
            });
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePeople(People people)
        {
            _logger.LogInformation("Creating people");

            return await HandleResponse(async () =>
            {
                return await _peopleService.CreatePeopleAsync(people);
            });
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePeople(string id, People people)
        {
            _logger.LogInformation("Update people");

            return await HandleResponse(async () =>
            {
                return await _peopleService.UpdateAsync(id, people);
            });
        }
    }
}