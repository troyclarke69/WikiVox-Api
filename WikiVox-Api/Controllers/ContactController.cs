using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Dtos;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;
        private IMapper _mapper;

        public ContactController(ContactService service, IMapper mapper)
        {
            _contactService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAll()
        {
            var Contact = await _contactService.GetAllAsync();
            return Ok(Contact);
        }

        //[HttpGet("{id:length(24)}", Name = "GetContact")]
        //public async Task<ActionResult<Contact>> GetById(string id)
        //{
        //    var Contact = await _contactService.GetByIdAsync(id);
        //    if (Contact == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(Contact);
        //}

        [HttpGet("{id:length(24)}", Name = "GetById")]
        public async Task<ActionResult<ContactReadDto>> GetById(string id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            //return Ok(Contact);
            return Ok(_mapper.Map<ContactReadDto>(contact));
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(Contact Contact)
        //{
        //    // got all required fields?
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    await _contactService.CreateAsync(Contact);
        //    return Ok(Contact);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateDto contactCreateDto)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contactModel = _mapper.Map<Contact>(contactCreateDto);
            await _contactService.CreateAsync(contactModel);

            var contactReadDto = _mapper.Map<ContactReadDto>(contactModel);

            // adheres to REST principles:
            // will pass back to 201: Created, AND the header:route (name)
            return CreatedAtRoute(nameof(GetById), new { Id = contactReadDto.Id }, contactReadDto);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Contact updatedContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedContact = await _contactService.GetByIdAsync(id);
            if (queriedContact == null)
            {
                return NotFound();
            }
            await _contactService.UpdateAsync(id, updatedContact);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Contact = await _contactService.GetByIdAsync(id);
            if (Contact == null)
            {
                return NotFound();
            }
            await _contactService.DeleteAsync(id);
            return NoContent();
        }
    }
}
