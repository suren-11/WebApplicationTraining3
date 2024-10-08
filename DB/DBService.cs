using MongoDB.Driver;
using WebApplicationTraining3.Entities;

namespace WebApplicationTraining3.DB
{
    public class DBService
    {
        private readonly IMongoCollection<SubjectStream> _streams;

        public DBService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("Dbconnection"));
            var database = client.GetDatabase("WebApiTraining");
            _streams = database.GetCollection<SubjectStream>("Streams");
        }

        public async Task<List<SubjectStream>> GetAllStreamsAsync()
        {
            return await _streams.Find(_ => true).ToListAsync();
        }
    }
}
