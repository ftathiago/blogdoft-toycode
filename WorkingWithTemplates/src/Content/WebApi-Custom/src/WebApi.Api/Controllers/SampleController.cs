using Microsoft.AspNetCore.Mvc;
using WebApi.Api.Model.Response;
using WebApi.Business.Services;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _service;

        public SampleController(ISampleService service) =>
            _service = service;

        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var sample = _service.GetSampleBy(id.Value);

            if (sample is null)
            {
                return NotFound();
            }

            return Ok(SampleResponse.From(sample));
        }
    }
}