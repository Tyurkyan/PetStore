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
        public ActionResult<IEnumerable<OwnerResponse>> GetAllOwners()
        {
            try
            {
                var owners = _ownerService.GetAllOwners();
                var ownerResponses = owners.Select(owner => _mapper.Map<OwnerResponse>(owner));
                return Ok(ownerResponses);
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching owners.");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetOwnerById(string id)
        {
            var owner = _ownerService.GetOwnerById(id);

            if (owner == null)
            {
                return NotFound();
            }

            var ownerResponse = _mapper.Map<OwnerResponse>(owner);
            return Ok(ownerResponse);
        }

        [HttpPost("AddOwner")]
        public ActionResult<OwnerResponse> AddOwner([FromBody] OwnerRequest ownerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var owner = _mapper.Map<Owner>(ownerRequest);
            _ownerService.AddOwner(owner);

            var ownerResponse = _mapper.Map<OwnerResponse>(owner);
            return CreatedAtAction(nameof(GetOwnerById), new { id = owner.Id }, ownerResponse);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteOwner(string id)
        {
            _ownerService.RemoveOwner(id);
            return NoContent();
        }
    }

}
