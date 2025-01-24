using PetStore.BL.Interfaces;
using PetStore.BL.Services;
using PetStore.Models.DTO;
using Moq;
using PetStore.DL.Interfaces;

namespace PetStore.Tests
{
    public class OwnerServiceUnitTest
    {
        private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
        private readonly List<Owner> _owners;

        public OwnerServiceUnitTest()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _owners = new List<Owner>
        {
            new Owner { Id = "1", Name = "Alice", PhoneNumber = "1234567890" },
            new Owner { Id = "2", Name = "Bob", PhoneNumber = "0987654321" }
        };
        }

        [Fact]
        public void GetAllOwners_ReturnsOwnerList()
        {
            // Arrange
            _ownerRepositoryMock.Setup(repo => repo.GetAll()).Returns(_owners);
            var ownerService = new OwnerService(_ownerRepositoryMock.Object);

            // Act
            var result = ownerService.GetAllOwners();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_owners.Count, result.Count);
        }

        [Fact]
        public void GetOwnerById_ReturnsOwner()
        {
            // Arrange
            var ownerId = "1";
            _ownerRepositoryMock.Setup(repo => repo.GetById(ownerId)).Returns(_owners.First(o => o.Id == ownerId));
            var ownerService = new OwnerService(_ownerRepositoryMock.Object);

            // Act
            var result = ownerService.GetOwnerById(ownerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ownerId, result.Id);
        }

        [Fact]
        public void AddOwner_CallsRepositoryCreate()
        {
            // Arrange
            var newOwner = new Owner { Id = "3", Name = "Charlie", PhoneNumber = "1122334455" };
            var ownerService = new OwnerService(_ownerRepositoryMock.Object);

            // Act
            ownerService.AddOwner(newOwner);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.Create(newOwner), Times.Once);
        }

        [Fact]
        public void RemoveOwner_CallsRepositoryDelete()
        {
            // Arrange
            var ownerId = "1";
            var ownerService = new OwnerService(_ownerRepositoryMock.Object);

            // Act
            ownerService.RemoveOwner(ownerId);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.Delete(ownerId), Times.Once);
        }
    }
}