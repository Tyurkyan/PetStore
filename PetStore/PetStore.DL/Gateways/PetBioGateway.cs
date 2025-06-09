using PetStore.Models.DTO;
using PetStore.Models.Response;
using RestSharp;
using PetStore.DL.Interfaces;

namespace PetStore.DL.Gateways
{
    public class PetBioGateway : IPetBioGateway
    {
        private readonly RestClient _client;

        public PetBioGateway()
        {
            _client = new RestClient("https://localhost:7178"); 
        }

        public async Task<PetBioResponse> GetPetBioInfo(string petId)
        {
            var request = new RestRequest($"/PetBio/{petId}", Method.Get);
            var response = await _client.ExecuteAsync<PetBioResponse>(request);

            return response.Data;
        }
    }
}
