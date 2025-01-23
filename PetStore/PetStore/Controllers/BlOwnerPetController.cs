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
        public IActionResult GetPetByOwnerId(string id)
        {
            var pets = _blOwnerPetService.GetPetByOwnerId(id);
            if (pets == null || !pets.Any())
            {
                return NotFound("No pets found for the given owner.");
            }

            var petResponses = pets.Select(pet => _mapper.Map<PetResponse>(pet));
            return Ok(petResponses);
        }
    }
}
