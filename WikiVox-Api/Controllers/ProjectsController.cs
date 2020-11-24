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
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsService _projectsService;

        public ProjectsController(ProjectsService service)
        {
            _projectsService = service;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Projects>>> GetAll()
        //{
        //    var Projects = await _projectsService.GetAllAsync();
        //    return Ok(Projects);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projects>>> GetAll()
        {
            var Projects = await _projectsService.GetAllAsync();
            return Ok(Projects);
        }

        [HttpGet("{id:length(24)}", Name = "GetProjects")]
        public async Task<ActionResult<Projects>> GetById(string id)
        {
            var Projects = await _projectsService.GetByIdAsync(id);
            if (Projects == null)
            {
                return NotFound();
            }
            return Ok(Projects);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Projects Projects)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _projectsService.CreateAsync(Projects);
            return Ok(Projects);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Projects updatedProjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedProjects = await _projectsService.GetByIdAsync(id);
            if (queriedProjects == null)
            {
                return NotFound();
            }
            await _projectsService.UpdateAsync(id, updatedProjects);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Projects = await _projectsService.GetByIdAsync(id);
            if (Projects == null)
            {
                return NotFound();
            }
            await _projectsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
