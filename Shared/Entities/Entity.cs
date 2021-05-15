using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Commom.Entities
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreatedAt = DateTime.Now;
        }
    }
}
