using AutoMapper;
using Core.Entities;
using MongoDB.Bson;

namespace Infrastructure.AutoMapper
{
    public class InfrastrutureProfile : Profile
    {
        public InfrastrutureProfile()
        {
            CreateMap<BsonDocument, People>();
        }
    }
}
