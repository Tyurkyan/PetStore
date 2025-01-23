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
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IMapper _mapper;

        public PetsController(IPetService petService, IMapper mapper)
        {
            _petService = petService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PetResponse>> GetAllPets()
        {
            try
            {
                var pets = _petService.GetAllPets();
                var petResponses = pets.Select(pet => _mapper.Map<PetResponse>(pet));
                return Ok(petResponses);
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching pets.");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetPetById(string id)
        {
            var pet = _petService.GetPetById(id);

            if (pet == null)
            {
                return NotFound();
            }

            var petResponse = _mapper.Map<PetResponse>(pet);
            return Ok(petResponse);
        }

        [HttpPost("AddPet")]
        public ActionResult<PetResponse> AddPet([FromBody] PetRequest petRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pet = _mapper.Map<Pet>(petRequest);
            _petService.AddPet(pet);

            var petResponse = _mapper.Map<PetResponse>(pet);
            return CreatedAtAction(nameof(GetPetById), new { id = pet.Id }, petResponse);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeletePet(string id)
        {
            _petService.RemovePet(id);
            return NoContent();
        }
    }
}
