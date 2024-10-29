using Application.Common.Helpers.Exceptions;
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

            return await _peopleRepository.InsertDocumentAsync(people) ??
                throw new BusinessException(BusinessExceptionType.ProblemSavingDatabase);
        }

        public async Task<People> GetByIdAsync(string id)
        {
            _logger.LogInformation("Method: {Method} -- Get people by id: {Id}",
                nameof(CreatePeopleAsync), id);

            return await _peopleRepository.GetByIdAsync(id) ??
                throw new BusinessException(BusinessExceptionType.ProblemGetData);
        }

        public async Task<People> UpdateAsync(string id, People people)
        {
            _logger.LogInformation("Updating people with the next id: {Id}  \nData: {Data}",
                id, SerializerObject.ConvertObjectToJsonIndented(people));

            bool responseUpdate = await _peopleRepository.UpdateAsync(id, people);

            _logger.LogInformation("Update status: {Status}", responseUpdate);

            People response = responseUpdate ? await _peopleRepository.GetByIdAsync(id) : null;

            return response;
        }

        public async Task<string> DeleteAsync(string id)
        {
            _logger.LogInformation("Deleting person with id: {Id}", id);

            bool responseDelete = await _peopleRepository.DeleteAsync(id);

            return responseDelete ?
                $"The person with id: {id} is deleted" :
                $"The person with id: {id} not it was deleted";
        }
    }
}