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

        public ControllerPeople(ILogger<ControllerPeople> logger, IPeopleService peopleService)
        {
            _logger = logger;
            _peopleService = peopleService;
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
    }
}