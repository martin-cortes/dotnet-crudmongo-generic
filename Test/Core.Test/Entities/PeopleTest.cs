using Core.Entities;
using Xunit;

namespace Core.Test.Entities
{
    public class PeopleTest
    {
        [Fact]
        public void SetPeopleEntity()
        {
            // Arrange

            People people = new PeopleBuilder().Build();

            // Assert

            Assert.NotNull(people);
            Assert.NotEmpty(people.Name);
            Assert.NotEmpty(people.City);
            Assert.NotEmpty(people.Region);
            Assert.NotEmpty(people.Address);
            Assert.NotEmpty(people.Country);
            Assert.NotEmpty(people.LastName);
            Assert.NotEmpty(people.PostalCode);
            Assert.NotEmpty(people.PhoneNumber);
            Assert.NotEmpty(people.IdDocumentNumber);
        }
    }
}