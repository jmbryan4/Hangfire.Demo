using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hangfire.Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireTestController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobs;

        public HangfireTestController(IBackgroundJobClient backgroundJobs)
        {
            _backgroundJobs = backgroundJobs;
        }

        [HttpGet]
        public IActionResult Get() => Ok("Hello!");

        [HttpGet("{jobs}")]
        public IActionResult HangfireTest(int jobs)
        {
            var time = Stopwatch.StartNew();
            for (int i = 0; i < jobs; i++)
            {
                _backgroundJobs.Enqueue(() => Job(i + 1));
            }
            time.Stop();
            return Ok($"{jobs} jobs queued in {time.Elapsed:c}");
        }

        [HttpGet("schedule/{jobs}")]
        public IActionResult HangfireTestScheduled(int jobs)
        {
            var time = Stopwatch.StartNew();
            for (int i = 0; i < jobs; i++)
            {
                _backgroundJobs.Schedule(() => Job(i + 1), TimeSpan.FromSeconds(20));
            }
            time.Stop();
            return Ok($"{jobs} jobs scheduled in {time.Elapsed:c}");
        }

        public void Job(int num)
        {
            Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Job {num}!");
        }
    }
}
