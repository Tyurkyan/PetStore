using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PetStore.BL.Interfaces;
using PetStore.Models.Response;

namespace PetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlOwnerPetController : ControllerBase
    {
        private readonly IBlOwnerPetService _blOwnerPetService;
        private readonly IMapper _mapper;

        public BlOwnerPetController(IBlOwnerPetService blOwnerPetService, IMapper mapper)
        {
            _blOwnerPetService = blOwnerPetService;
            _mapper = mapper;
        }

        [HttpGet("GetPetByOwnerId/{id}")]
        public async Task<IActionResult> GetPetByOwnerId(string id)
        {
            var pets = await _blOwnerPetService.GetPetByOwnerIdAsync(id);

            if (pets == null || !pets.Any())
                return NotFound("No pets found for the given owner.");

            var petResponses = pets.Select(_mapper.Map<PetResponse>);
            return Ok(petResponses);
        }
    }
}
