using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherRepository _repository;

        public WeatherController(WeatherRepository repository)
        {
            _repository = repository;
        }

        // GET: api/weather
        [HttpGet]
        public IActionResult GetAll()
        {
            var weather = _repository.GetAll();
            return Ok(weather);
        }

        // GET: api/weather/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var weather = _repository.GetById(id);
            if (weather == null)
            {
                return NotFound();
            }
            return Ok(weather);
        }

        // POST: api/weather
        [HttpPost("Reg")]
        public IActionResult Post([FromBody] Weather weather)
        {
            if (weather == null)
            {
                return BadRequest();
            }

            _repository.Insert(weather);
            return CreatedAtAction(nameof(GetById), new { id = weather.Id }, weather);
        }

        // PUT: api/weather/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Weather weather)
        {
            if (id != weather.Id)
            {
                return BadRequest();
            }

            var existingWeather = _repository.GetById(id);
            if (existingWeather == null)
            {
                return NotFound();
            }

            _repository.Update(weather);
            return NoContent();
        }

        // DELETE: api/weather/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var weather = _repository.GetById(id);
            if (weather == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            return NoContent();
        }
    }
}
