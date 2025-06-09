using Microsoft.AspNetCore.Mvc;

namespace PetBioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetBioController : ControllerBase
    {
        private static readonly string[] Breeds = new[] { "Labrador", "Bulldog", "Beagle", "Poodle" };
        private static readonly string[] Temperaments = new[] { "Calm", "Playful", "Protective", "Friendly" };

        [HttpGet("{petId}")]
        public IActionResult GetPetBio(string petId)
        {
            var random = Random.Shared.Next(Breeds.Length);
            return Ok(new PetStore.Models.Response.PetBioResponse
            {
                PetId = petId,
                Breed = Breeds[random],
                Temperament = Temperaments[random],
                IdealDailyCalories = 500 + random * 100
            });
        }
    }
}
