
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MongoDB.Driver;
using WebApplicationTraining3.DB;
using WebApplicationTraining3.Entities;

namespace WebApplicationTraining3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly DBService _dbService;

        public StreamController(DBService dBService)
        {
            _dbService = dBService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStreams()
        {
            var streams = await _dbService.GetAllStreamsAsync();
            return Ok(streams);
        }
    }
}
