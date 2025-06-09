using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PetStore.BL.Interfaces;
using PetStore.Models.DTO;
using PetStore.Models.Request;
using PetStore.Models.Response;

namespace PetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllOwners()
        {
            try
            {
                var owners = await _ownerService.GetAllOwnersAsync();
                var ownerResponses = owners.Select(_mapper.Map<OwnerResponse>);
                return Ok(ownerResponses);
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching owners.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetOwnerById(string id)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);

            if (owner == null) return NotFound();

            return Ok(_mapper.Map<OwnerResponse>(owner));
        }

        [HttpPost("AddOwner")]
        public async Task<ActionResult<OwnerResponse>> AddOwner([FromBody] OwnerRequest ownerRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var owner = _mapper.Map<Owner>(ownerRequest);
            await _ownerService.AddOwnerAsync(owner);

            var ownerResponse = _mapper.Map<OwnerResponse>(owner);
            return CreatedAtAction(nameof(GetOwnerById), new { id = owner.Id }, ownerResponse);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOwner(string id)
        {
            await _ownerService.RemoveOwnerAsync(id);
            return NoContent();
        }
    }

}
