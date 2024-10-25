using Application.Interfaces.Infrastructure;
using AutoMapper;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public class PeopleRepository<TEntity> : IPeopleRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public PeopleRepository(MongoContext context, IMapper mapper)
        {
            _collection = context.GetCollection();
        }

        public async Task<string> InsertDocumentAsync(TEntity entity)
        {
            BsonDocument document = entity.ToBsonDocument();

            await _collection.InsertOneAsync(document);

            ObjectId bsonId = document["_id"].AsObjectId;

            return bsonId.ToString();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            BsonDocument document = await _collection.Find(filter).FirstOrDefaultAsync();

            document.Remove("_id");

            return BsonSerializer.Deserialize<TEntity>(document);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IEnumerable<BsonDocument> bsonElements = await _collection.Find(new BsonDocument()).ToListAsync();

            return BsonSerializer.Deserialize<IEnumerable<TEntity>>((BsonDocument)bsonElements);
        }

        public async Task<bool> UpdateAsync(string id, TEntity entity)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            BsonDocument document = entity.ToBsonDocument();

            ReplaceOneResult response = await _collection.ReplaceOneAsync(filter, document);

            return response.IsModifiedCountAvailable;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            DeleteResult respose = await _collection.DeleteOneAsync(filter);

            return respose.IsAcknowledged;
        }
    }
}