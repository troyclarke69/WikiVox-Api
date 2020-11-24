using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly EntityService _entityService;

        public EntityController(EntityService service)
        {
            _entityService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entity>>> GetAll()
        {
            var Entity = await _entityService.GetAllAsync();
            return Ok(Entity);
        }

        [HttpGet("{id:length(24)}", Name = "GetEntity")]
        public async Task<ActionResult<Entity>> GetById(string id)
        {
            var Entity = await _entityService.GetByIdAsync(id);
            if (Entity == null)
            {
                return NotFound();
            }

            return Ok(Entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Entity Entity)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _entityService.CreateAsync(Entity);
            return Ok(Entity);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Entity updatedEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedEntity = await _entityService.GetByIdAsync(id);
            if (queriedEntity == null)
            {
                return NotFound();
            }
            await _entityService.UpdateAsync(id, updatedEntity);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Entity = await _entityService.GetByIdAsync(id);
            if (Entity == null)
            {
                return NotFound();
            }
            await _entityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
