using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficController : ControllerBase
    {
        private readonly TrafficService _trafficService;

        public TrafficController(TrafficService service)
        {
            _trafficService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Traffic>>> GetAll()
        {
            var Traffic = await _trafficService.GetAllAsync();
            return Ok(Traffic);
        }

        [HttpGet("{id:length(24)}", Name = "GetTraffic")]
        public async Task<ActionResult<Traffic>> GetById(string id)
        {
            var Traffic = await _trafficService.GetByIdAsync(id);
            if (Traffic == null)
            {
                return NotFound();
            }
            return Ok(Traffic);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Traffic traffic)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _trafficService.CreateAsync(traffic);
            return Ok(traffic);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Traffic updatedTraffic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedTraffic = await _trafficService.GetByIdAsync(id);
            if (queriedTraffic == null)
            {
                return NotFound();
            }
            await _trafficService.UpdateAsync(id, updatedTraffic);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Traffic = await _trafficService.GetByIdAsync(id);
            if (Traffic == null)
            {
                return NotFound();
            }
            await _trafficService.DeleteAsync(id);
            return NoContent();
        }
    }
}
