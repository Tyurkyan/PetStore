using Moq;
using PetStore.BL.Services;
using PetStore.DL.Interfaces;
using PetStore.Models.DTO;

namespace PetStore.Tests
{
    public class PetServiceUnitTest
    {
        private readonly Mock<IPetRepository> _petRepositoryMock;
        private readonly List<Pet> _pets;

        public PetServiceUnitTest()
        {
            _petRepositoryMock = new Mock<IPetRepository>();
            _pets = new List<Pet>
        {
            new Pet { Id = "1", Type = "Dog", Age = 3, Price = 500.00m, OwnerId = "1" },
            new Pet { Id = "2", Type = "Cat", Age = 2, Price = 300.00m, OwnerId = "2" }
        };
        }

        [Fact]
        public void GetAllPets_ReturnsPetList()
        {
            // Arrange
            _petRepositoryMock.Setup(repo => repo.GetAll()).Returns(_pets);
            var petService = new PetService(_petRepositoryMock.Object);

            // Act
            var result = petService.GetAllPets();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_pets.Count, result.Count);
        }

        [Fact]
        public void GetPetById_ReturnsPet()
        {
            // Arrange
            var petId = "1";
            _petRepositoryMock.Setup(repo => repo.GetById(petId)).Returns(_pets.First(p => p.Id == petId));
            var petService = new PetService(_petRepositoryMock.Object);

            // Act
            var result = petService.GetPetById(petId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(petId, result.Id);
        }

        [Fact]
        public void AddPet_CallsRepositoryCreate()
        {
            // Arrange
            var newPet = new Pet { Id = "3", Type = "Bird", Age = 1, Price = 150.00m, OwnerId = "1" };
            var petService = new PetService(_petRepositoryMock.Object);

            // Act
            petService.AddPet(newPet);

            // Assert
            _petRepositoryMock.Verify(repo => repo.Create(newPet), Times.Once);
        }

        [Fact]
        public void RemovePet_CallsRepositoryDelete()
        {
            // Arrange
            var petId = "1";
            var petService = new PetService(_petRepositoryMock.Object);

            // Act
            petService.RemovePet(petId);

            // Assert
            _petRepositoryMock.Verify(repo => repo.Delete(petId), Times.Once);
        }
    }
}
