using Core.Entities;

namespace Core.Test.Entities
{
    public class PeopleBuilder
    {
        private string _name;
        private string _city;
        private string _region;
        private string _address;
        private string _country;
        private string _lastName;
        private string _postalCode;
        private string _phoneNumber;
        private string _idDocumentNumber;

        public PeopleBuilder()
        {
            _name = "Martin";
            _city = "Medellin";
            _region = "Antioquia";
            _address = "calle 99 # 56 - 41";
            _country = "Colombia";
            _lastName = "Cortes";
            _postalCode = "050040";
            _phoneNumber = "3245429065";
            _idDocumentNumber = "12345";
        }

        public PeopleBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PeopleBuilder WithCity(string city)
        {
            _city = city;
            return this;
        }

        public PeopleBuilder WithRegion(string region)
        {
            _region = region;
            return this;
        }

        public PeopleBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public PeopleBuilder WithCountry(string country)
        {
            _country = country;
            return this;
        }

        public PeopleBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public PeopleBuilder WithPostalCode(string postalCode)
        {
            _postalCode = postalCode;
            return this;
        }

        public PeopleBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public PeopleBuilder WithIdDocumentNumber(string idDocuemntNumber)
        {
            _idDocumentNumber = idDocuemntNumber;
            return this;
        }

        public People Build()
        {
            return new()
            {
                Name = _name,
                City = _city,
                Region = _region,
                Address = _address,
                Country = _country,
                LastName = _lastName,
                PostalCode = _postalCode,
                PhoneNumber = _phoneNumber,
                IdDocumentNumber = _idDocumentNumber
            };
        }
    }
}