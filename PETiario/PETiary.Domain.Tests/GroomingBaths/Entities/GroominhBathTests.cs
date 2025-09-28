using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.GroomingBaths.Entities;
using PETiario.PETiary.Domain.GroomingBaths.Enumerators;
using Xunit;

namespace PETiario.PETiary.Domain.Tests.GroomingBaths.Entities
{
    public class GroominhBathTests
    {
        private static List<ValidationResult> ValidateModel(object model)
        {
            List<ValidationResult> results = [];
            ValidationContext context = new (model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void GroomingBath_WithValidData_ShouldBeValid()
        {
            var bath = new GroomingBath(
                petId: 1,
                date: DateTime.Now,
                kind: KindEnum.Bath,
                location: "Vet",
                note: "All ok"
            );

            var results = ValidateModel(bath);

            Assert.Empty(results);
        }

        [Fact]
        public void GroomingBath_WithInvalidPetId_ShouldFailValidation()
        {
            var bath = new GroomingBath(
                petId: 0, 
                date: DateTime.Now,
                kind: KindEnum.Bath,
                location: "Local",
                note: "Note"
            );

            var results = ValidateModel(bath);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Pet can not be null"));
        }

        [Fact]
        public void GroomingBath_WithShortLocation_ShouldFailValidation()
        {
            var bath = new GroomingBath(
                petId: 1,
                date: DateTime.Now,
                kind: KindEnum.Bath,
                location: "Lo", 
                note: "Note"
            );

            var results = ValidateModel(bath);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Location's minimum length"));
        }

        [Fact]
        public void GroomingBath_WithLongNote_ShouldFailValidation()
        {
            var bath = new GroomingBath(
                petId: 1,
                date: DateTime.Now,
                kind: KindEnum.Grooming,
                location: "Room",
                note: new string('a', 101) 
            );

            var results = ValidateModel(bath);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Note's maximum length"));
        }

        [Fact]
        public void GroomingBath_WithMissingDate_ShouldFailValidation()
        {
            var bath = new GroomingBath(
                petId: 1,
                date: default, 
                kind: KindEnum.Bath,
                location: "Local",
                note: "Note"
            );

            var results = ValidateModel(bath);

            Assert.Contains(results, r => r.MemberNames.Contains("Date"));
        }

        [Fact]
        public void GroomingBath_WithInvalidKind_ShouldFailValidation()
        {
            var bath = new GroomingBath(
                petId: 1,
                date: DateTime.Now,
                kind: 0,
                location: "Local",
                note: "Note"
            );

            var results = ValidateModel(bath);

            Assert.Contains(results, r => r.MemberNames.Contains("Kind"));
        }
        
        [Fact]
        public void GroomingBath_WithMinDate_ShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                GroomingBath grooming = new GroomingBath(
                    petId: 1,
                    date: DateTime.MinValue,
                    kind: KindEnum.Bath,
                    location: "PetShop",
                    note: "Ok"
                );
            });
        }

    }
}
