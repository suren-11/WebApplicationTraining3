using MongoDB.Bson.Serialization.Attributes;

namespace WebApplicationTraining3.Entities
{
    public class SubjectStream
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ObjectId { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("prefix")]
        public string Prefix { get; set; }

    }
}
