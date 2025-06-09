using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PetStore.BL.Interfaces;
using PetStore.DL.Interfaces;
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
        private readonly IPetBioGateway _petBioGateway;

        public PetsController(IPetService petService, IMapper mapper, IPetBioGateway petBioGateway)
        {
            _petService = petService;
            _mapper = mapper;
            _petBioGateway = petBioGateway;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PetResponse>>> GetAllPets()
        {
            try
            {
                var pets = await _petService.GetAllPetsAsync();
                var petResponses = pets.Select(_mapper.Map<PetResponse>);
                return Ok(petResponses);
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching pets.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetPetById(string id)
        {
            var pet = await _petService.GetPetByIdAsync(id);

            if (pet == null) return NotFound();

            return Ok(_mapper.Map<PetResponse>(pet));
        }

        [HttpPost("AddPet")]
        public async Task<ActionResult<PetResponse>> AddPet([FromBody] PetRequest petRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pet = _mapper.Map<Pet>(petRequest);
            await _petService.AddPetAsync(pet);

            var petResponse = _mapper.Map<PetResponse>(pet);
            return CreatedAtAction(nameof(GetPetById), new { id = pet.Id }, petResponse);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePet(string id)
        {
            await _petService.RemovePetAsync(id);
            return NoContent();
        }


        [HttpGet("{id}/bio")]
        public async Task<IActionResult> GetPetBio(string id)
        {
            try
            {
                var petBio = await _petBioGateway.GetPetBioInfo(id);

                if (petBio == null)
                    return NotFound();

                return Ok(petBio);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
