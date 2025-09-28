
using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.Walks.Entities;
using Xunit;

namespace PETiario.PETiary.Domain.Tests.Walks.Entities
{
    public class WalkTests
    {
        private static List<ValidationResult> ValidateModel(object model)
        {
            List<ValidationResult> results = [];
            ValidationContext context = new(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Walk_WithValidData_ShouldBeValid()
        {
            Walk walk = new(
                petId: 1,
                date: DateTime.Today,
                durationMinutes: 30,
                note: "All ok",
                responsible: "João"
            );

            IList<ValidationResult> results = ValidateModel(walk);

            Assert.Empty(results);
        }

        [Fact]
        public void Walk_WithInvalidPetId_ShouldFailValidation()
        {
            Walk walk = new(
                petId: 0,
                date: DateTime.Today,
                durationMinutes: 30,
                note: "All ok",
                responsible: "João"
            );

            IList<ValidationResult> results = ValidateModel(walk);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Pet can not be null"));
        }

        [Fact]
        public void Walk_WithNullResponsible_ShouldFailValidation()
        {
            Walk walk = new(
                petId: 1,
                date: DateTime.Today,
                durationMinutes: 30,
                note: "Any note",
                responsible: null
            );

            IList<ValidationResult> results = ValidateModel(walk);

            Assert.Contains(results, result => result.MemberNames.Contains("Responsible"));
        }

        [Fact]
        public void Walk_WithTooLongResponsible_ShouldFailValidation()
        {
            string longResponsible = new('a', 101);

            Walk walk = new(
                petId: 1,
                date: DateTime.Today,
                durationMinutes: 30,
                note: "Valid note",
                responsible: longResponsible
            );

            IList<ValidationResult> results = ValidateModel(walk);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Responsible's maximum length"));
        }

        [Fact]
        public void Walk_WithShortNote_ShouldFailValidation()
        {
            Walk walk = new(
                petId: 1,
                date: DateTime.Today,
                durationMinutes: 30,
                note: "ok",
                responsible: "Maria"
            );

            IList<ValidationResult> results = ValidateModel(walk);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Note's minimum length"));
        }

        [Fact]
        public void Walk_WithTooLongNote_ShouldFailValidation()
        {
            string longNote = new('x', 101);

            Walk walk = new(
                petId: 1,
                date: DateTime.Today,
                durationMinutes: 30,
                note: longNote,
                responsible: "Pedro"
            );

            IList<ValidationResult> results = ValidateModel(walk);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Note's maximum length"));
        }

        [Fact]
        public void Walk_WithMinDate_ShouldNotChangeDate()
        {
            DateTime originalDate = DateTime.Today;

            Walk walk = new(
                petId: 1,
                date: originalDate,
                durationMinutes: 20,
                note: "Ok",
                responsible: "Pedro"
            );

            walk.ChangeDate(DateTime.MinValue);

            Assert.Equal(originalDate, walk.Date); 
        }
    }
}