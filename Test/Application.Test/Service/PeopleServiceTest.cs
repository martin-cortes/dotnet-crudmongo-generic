using Application.Common.Helpers.Exceptions;
using Application.Interfaces.Infrastructure;
using Application.Service;
using Core.Entities;
using Core.Test.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Application.Test.Service
{
    public class PeopleServiceTest
    {
        private readonly MockRepository _mockRepository;
        private readonly ILogger<PeopleService> _logger;
        private readonly Mock<IPeopleRepository<People>> _peopleRepository;

        public PeopleServiceTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _logger = new NullLogger<PeopleService>();
            _peopleRepository = _mockRepository.Create<IPeopleRepository<People>>();
        }

        private PeopleService CreatePeopleService()
        {
            return new PeopleService(_logger, _peopleRepository.Object);
        }

        #region Create People

        [Fact]
        public async Task Create_People_Successful()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            People people = new PeopleBuilder().Build();

            string idResponse = Guid.NewGuid().ToString();

            _peopleRepository.Setup(_ => _
            .InsertDocumentAsync(It.Is<People>(x => x.Name == people.Name &&
                                                    x.City == people.City &&
                                                    x.Region == people.Region &&
                                                    x.Address == people.Address &&
                                                    x.Country == people.Country &&
                                                    x.LastName == people.LastName &&
                                                    x.PostalCode == people.PostalCode &&
                                                    x.PhoneNumber == people.PhoneNumber &&
                                                    x.IdDocumentNumber == people.IdDocumentNumber))
            )
            .ReturnsAsync(idResponse)
            .Verifiable();

            // Act

            await peopleService.CreatePeopleAsync(people);

            // Assert

            _peopleRepository.Verify(_ => _.InsertDocumentAsync(It.IsAny<People>()), Times.Once());

            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Failure_To_Create_People()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            People people = new PeopleBuilder().Build();

            _peopleRepository.Setup(_ => _
            .InsertDocumentAsync(It.Is<People>(x => x.Name == people.Name &&
                                                    x.City == people.City &&
                                                    x.Region == people.Region &&
                                                    x.Address == people.Address &&
                                                    x.Country == people.Country &&
                                                    x.LastName == people.LastName &&
                                                    x.PostalCode == people.PostalCode &&
                                                    x.PhoneNumber == people.PhoneNumber &&
                                                    x.IdDocumentNumber == people.IdDocumentNumber))
            )
            .ThrowsAsync(new BusinessException(BusinessExceptionType.ProblemSavingDatabase))
            .Verifiable();

            // Act

            BusinessException businessException = await Assert.ThrowsAsync<BusinessException>(async () =>
            await peopleService.CreatePeopleAsync(people));

            // Assert

            Assert.NotNull(businessException);

            Assert.Equal(new BusinessException(BusinessExceptionType.ProblemSavingDatabase).Message, businessException.Message);
        }

        [Fact]
        public async Task Create_People_Response_Null()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            People people = new PeopleBuilder().Build();

            string idResponse = null;

            _peopleRepository.Setup(_ => _
            .InsertDocumentAsync(It.Is<People>(x => x.Name == people.Name &&
                                                    x.City == people.City &&
                                                    x.Region == people.Region &&
                                                    x.Address == people.Address &&
                                                    x.Country == people.Country &&
                                                    x.LastName == people.LastName &&
                                                    x.PostalCode == people.PostalCode &&
                                                    x.PhoneNumber == people.PhoneNumber &&
                                                    x.IdDocumentNumber == people.IdDocumentNumber))
            )
            .ReturnsAsync(idResponse)
            .Verifiable();

            // Act

            BusinessException businessException = await Assert.ThrowsAsync<BusinessException>(async () =>
            await peopleService.CreatePeopleAsync(people));

            // Assert

            Assert.NotNull(businessException);

            Assert.Equal(new BusinessException(BusinessExceptionType.ProblemSavingDatabase).Message, businessException.Message);
        }

        #endregion Create People

        #region Get By Id

        [Fact]
        public async Task Get_People_By_Id_Successful()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            string id = Guid.NewGuid().ToString();

            People people = new PeopleBuilder().Build();

            _peopleRepository.Setup(_ => _
            .GetByIdAsync(It.Is<string>(x => x == id)))
                .ReturnsAsync(people)
                .Verifiable();

            // Act

            await peopleService.GetByIdAsync(id);

            // Assert

            _peopleRepository.Verify(_ => _.GetByIdAsync(It.IsAny<string>()), Times.Once());

            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Failure_To_Get_People_By_Id()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            string id = Guid.NewGuid().ToString();

            _peopleRepository.Setup(_ => _
            .GetByIdAsync(It.Is<string>(x => x == id)))
                .ThrowsAsync(new BusinessException(BusinessExceptionType.ProblemGetData))
                .Verifiable();

            // Act

            BusinessException businessException = await Assert.ThrowsAsync<BusinessException>(async () =>
            await peopleService.GetByIdAsync(id));

            // Assert

            Assert.NotNull(businessException);

            Assert.Equal(new BusinessException(BusinessExceptionType.ProblemGetData).Message, businessException.Message);
        }

        [Fact]
        public async Task Get_People_By_Id_Response_Null()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            string id = Guid.NewGuid().ToString();

            People people = null;

            _peopleRepository.Setup(_ => _
            .GetByIdAsync(It.Is<string>(x => x == id)))
                .ReturnsAsync(people)
                .Verifiable();

            // Act

            BusinessException businessException = await Assert.ThrowsAsync<BusinessException>(async () =>
            await peopleService.GetByIdAsync(id));

            // Assert

            Assert.NotNull(businessException);

            Assert.Equal(new BusinessException(BusinessExceptionType.ProblemGetData).Message, businessException.Message);
        }

        #endregion Get By Id

        #region Update People

        [Fact]
        public async Task Update_People_Successful()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            string id = Guid.NewGuid().ToString();

            People people = new PeopleBuilder().Build();

            _peopleRepository.Setup(_ =>
            _.UpdateAsync(It.Is<string>(x => x == id),
                          It.Is<People>(x =>
                          x.Name == people.Name &&
                          x.City == people.City &&
                          x.Region == people.Region &&
                          x.Address == people.Address &&
                          x.Country == people.Country &&
                          x.LastName == people.LastName &&
                          x.PostalCode == people.PostalCode &&
                          x.PhoneNumber == people.PhoneNumber &&
                          x.IdDocumentNumber == people.IdDocumentNumber))
            )
            .ReturnsAsync(true)
            .Verifiable();

            _peopleRepository.Setup(_ => _.GetByIdAsync(It.Is<string>(x => x == id)))
                .ReturnsAsync(people)
                .Verifiable();

            // Act

            await peopleService.UpdateAsync(id, people);

            // Assert

            _peopleRepository.Verify(_ => _.UpdateAsync(It.IsAny<string>(), It.IsAny<People>()), Times.Once());

            _peopleRepository.Verify(_ => _.GetByIdAsync(It.IsAny<string>()), Times.Once());

            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_People_Response_Null()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            string id = Guid.NewGuid().ToString();

            People people = new PeopleBuilder().Build();

            _peopleRepository.Setup(_ =>
            _.UpdateAsync(It.Is<string>(x => x == id),
                          It.Is<People>(x =>
                          x.Name == people.Name &&
                          x.City == people.City &&
                          x.Region == people.Region &&
                          x.Address == people.Address &&
                          x.Country == people.Country &&
                          x.LastName == people.LastName &&
                          x.PostalCode == people.PostalCode &&
                          x.PhoneNumber == people.PhoneNumber &&
                          x.IdDocumentNumber == people.IdDocumentNumber))
            )
            .ReturnsAsync(false)
            .Verifiable();

            // Act

            People response = await peopleService.UpdateAsync(id, people);

            // Assert

            _peopleRepository.Verify(_ => _.UpdateAsync(It.IsAny<string>(), It.IsAny<People>()), Times.Once());

            Assert.Null(response);

            _mockRepository.VerifyAll();
        }

        #endregion Update People

        #region Delete

        [Fact]
        public async Task Delete_People_Successful()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            string id = Guid.NewGuid().ToString();

            _peopleRepository.Setup(_ => _.DeleteAsync(It.Is<string>(x => x == id)))
                .ReturnsAsync(true)
                .Verifiable();

            // Act

            string response = await peopleService.DeleteAsync(id);

            // Assert

            Assert.NotNull(response);

            Assert.NotEmpty(response);

            _peopleRepository.Verify(_ => _.DeleteAsync(It.IsAny<string>()), Times.Once());

            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task People_Not_It_Was_Deleted()
        {
            // Arrange

            PeopleService peopleService = CreatePeopleService();

            string id = Guid.NewGuid().ToString();

            _peopleRepository.Setup(_ => _.DeleteAsync(It.Is<string>(x => x == id)))
                .ReturnsAsync(false)
                .Verifiable();

            // Act

            string response = await peopleService.DeleteAsync(id);

            // Assert

            Assert.NotNull(response);

            Assert.NotEmpty(response);

            _peopleRepository.Verify(_ => _.DeleteAsync(It.IsAny<string>()), Times.Once());

            _mockRepository.VerifyAll();
        }

        #endregion Delete
    }
}