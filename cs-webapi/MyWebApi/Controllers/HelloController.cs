using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data; // Namespace for ApiDbContext
using MyWebApi.Models; // Namespace for RequestLog
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        // private static readonly string LogDirectory = "/app/logs";
        // private static readonly string LogFile = Path.Combine(LogDirectory, "hello-requests.log");
        // private static readonly string CountFile = Path.Combine(LogDirectory, "request-count.txt");

        private readonly ApiDbContext _context;

        public HelloController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Directory.CreateDirectory(LogDirectory);

            await LogRequest();

            var count = await _context.RequestLogs.CountAsync();

            return Ok("Hello, World! (Requested " + count + " times)");
        }

        private async Task LogRequest()
        {
            // var updatedRequestCount = UpdateCounter();

            // var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC - Request to /Hello. Count: {updatedRequestCount}";
            // System.IO.File.AppendAllText(LogFile, logEntry + Environment.NewLine);

            var log = new RequestLog
            {
                Timestamp = DateTime.UtcNow,
                Endpoint = "/Hello"
            };
            _context.RequestLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        // private int UpdateCounter()
        // {
        //     var count = 0;
        //     if (System.IO.File.Exists(CountFile))
        //     {
        //         var countText = System.IO.File.ReadAllText(CountFile);
        //         int.TryParse(countText, out count);
        //     }
        //     count++;
        //     System.IO.File.WriteAllText(CountFile, count.ToString());

        //     return count;
        // }

        // Retrieves all logged requests
        [HttpGet("Logs")]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _context.RequestLogs
                .OrderByDescending(log => log.Timestamp)
                .ToListAsync();

            return Ok(logs);
        }
    }
}
