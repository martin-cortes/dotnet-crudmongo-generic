using Application.Common.Helpers.Serializer;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Service
{
    public class PeopleService : IPeopleService
    {
        private readonly ILogger<PeopleService> _logger;
        private readonly IPeopleRepository<People> _peopleRepository;

        public PeopleService(ILogger<PeopleService> logger, IPeopleRepository<People> peopleRepository)
        {
            _logger = logger;
            _peopleRepository = peopleRepository;
        }

        public async Task<string> CreatePeopleAsync(People people)
        {
            _logger.LogInformation("Method: {Method} -- Data insertion begins: {Data}",
                nameof(CreatePeopleAsync), SerializerObject.ConvertObjectToJsonIndented(people));

            return await _peopleRepository.InsertDocumentAsync(people);
        }

        public async Task<People> GetByIdAsync(string id)
        {
            _logger.LogInformation("Method: {Method} -- Get people by id: {Id}",
                nameof(CreatePeopleAsync), id);

            return await _peopleRepository.GetByIdAsync(id);    
        }
    }
}
