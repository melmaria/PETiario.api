
using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.Pets.Entities;
using PETiario.PETiary.Domain.Pets.Enumerators;
using Xunit;

namespace PETiario.PETiary.Domain.Tests.Pets.Entities
{
    public class PetTests
    {
        private  static List<ValidationResult> ValidateModel(object model)
        {
            List<ValidationResult> results = [];
            ValidationContext context = new(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Pet_WithValidData_ShouldBeValid()
        {
            Pet pet = new (
                name: "Rex",
                species: SpeciesEnum.Dog,
                breed: "Labrador",
                age: 5,
                gender: GenderEnum.Male,
                picture: "rex.jpg",
                idOwner: 1
            );

            var results = ValidateModel(pet);

            Assert.Empty(results);
        }

        [Fact]
        public void Pet_WithNullName_ShouldFailValidation()
        {
            Pet pet = new (
                name: null,
                species: SpeciesEnum.Cat,
                breed: "Siamese",
                age: 3,
                gender: GenderEnum.Female,
                picture: "cat.jpg",
                idOwner: 1
            );

            var results = ValidateModel(pet);

            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Pet_WithTooLongName_ShouldFailValidation()
        {
            var longName = new string('a', 101);

            Pet pet = new (
                name: longName,
                species: SpeciesEnum.Dog,
                breed: "Bulldog",
                age: 2,
                gender: GenderEnum.Male,
                picture: "dog.jpg",
                idOwner: 1
            );

            var results = ValidateModel(pet);

            Assert.Contains(results, r => r.ErrorMessage.Contains("maximum length"));
        }

        [Fact]
        public void Pet_WithNullAge_ShouldFailValidation()
        {
            Pet pet = new (
                name: "Luna",
                species: SpeciesEnum.Cat,
                breed: "Persian",
                age: 0,
                gender: GenderEnum.Female,
                picture: "luna.jpg",
                idOwner: 1
            );

            var results = ValidateModel(pet);

            Assert.Contains(results, r => r.MemberNames.Contains("Age"));
        }

        [Fact]
        public void Pet_WithInvalidBreed_ShouldFailValidation()
        {
            Pet pet = new (
                name: "Luna",
                species: SpeciesEnum.Cat,
                breed: "",
                age: 2,
                gender: GenderEnum.Female,
                picture: "luna.jpg",
                idOwner: 1
            );

            var results = ValidateModel(pet);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Breed can not be null"));
        }

        [Fact]
        public void Pet_WithInvalidSpecies_ShouldFailValidation()
        {
            Pet pet = new (
                name: "Luna",
                species: 0,
                breed: "Persian",
                age: 2,
                gender: GenderEnum.Female,
                picture: "luna.jpg",
                idOwner: 1
            );

            var results = ValidateModel(pet);

            Assert.Contains(results, r => r.MemberNames.Contains("Species"));
        }

        [Fact]
        public void Pet_WithInvalidGender_ShouldFailValidation()
        {
            Pet pet = new (
                name: "Luna",
                species: SpeciesEnum.Cat,
                breed: "Persian",
                age: 2,
                gender: 0,
                picture: "luna.jpg",
                idOwner: 1
            );

            var results = ValidateModel(pet);

            Assert.Contains(results, r => r.MemberNames.Contains("Gender"));
        }
    }
}