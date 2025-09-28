
using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.Vaccinations.Entities;
using Xunit;

namespace PETiario.PETiary.Domain.Tests.Vaccionations.Entities
{
    public class VaccinationTests
    {

        private static List<ValidationResult> ValidateModel(object model)
        {
            List<ValidationResult> results = [];
            ValidationContext context = new(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Vaccination_WithValidData_ShouldBeValid()
        {
            Vaccination vaccination = new(
                petId: 1,
                vaccinate: "Ranger",
                shotDate: DateTime.Today,
                note: "Aplicada com sucesso",
                nextShot: DateTime.Today.AddMonths(12),
                applied: true
            );

            IList<ValidationResult> results = ValidateModel(vaccination);

            Assert.Empty(results);
        }

        [Fact]
        public void Vaccination_WithInvalidPetId_ShouldFailValidation()
        {
            Vaccination vaccination = new(
                petId: 0,
                vaccinate: "V10",
                shotDate: DateTime.Today,
                note: "OK",
                nextShot: DateTime.Today.AddMonths(6)
            );

            IList<ValidationResult> results = ValidateModel(vaccination);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Pet can not be null"));
        }

        [Fact]
        public void Vaccination_WithNullVaccinate_ShouldFailValidation()
        {
            Vaccination vaccination = new(
                petId: 1,
                vaccinate: null,
                shotDate: DateTime.Today,
                note: "OK",
                nextShot: DateTime.Today.AddMonths(6)
            );

            IList<ValidationResult> results = ValidateModel(vaccination);

            Assert.Contains(results, result => result.MemberNames.Contains("Vaccinate"));
        }

        [Fact]
        public void Vaccination_WithTooLongVaccinate_ShouldFailValidation()
        {
            string longVaccinate = new('a', 101);

            Vaccination vaccination = new(
                petId: 1,
                vaccinate: longVaccinate,
                shotDate: DateTime.Today,
                note: "OK",
                nextShot: DateTime.Today.AddMonths(6)
            );

            IList<ValidationResult> results = ValidateModel(vaccination);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Vaccinate's maximum length"));
        }

        [Fact]
        public void Vaccination_WithDefaultShotDate_ShouldFailValidation()
        {
            Vaccination vaccination = new(
                petId: 1,
                vaccinate: "V10",
                shotDate: default,
                note: "OK",
                nextShot: DateTime.Today.AddMonths(6)
            );

            IList<ValidationResult> results = ValidateModel(vaccination);

            Assert.Contains(results, result => result.MemberNames.Contains("ShotDate"));
        }

        [Fact]
        public void Vaccination_WithShortNote_ShouldFailValidation()
        {
            Vaccination vaccination = new(
                petId: 1,
                vaccinate: "V10",
                shotDate: DateTime.Today,
                note: "ok",
                nextShot: DateTime.Today.AddMonths(6)
            );

            IList<ValidationResult> results = ValidateModel(vaccination);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Note's minimum length"));
        }

        [Fact]
        public void Vaccination_WithTooLongNote_ShouldFailValidation()
        {
            string longNote = new('a', 101);

            Vaccination vaccination = new(
                petId: 1,
                vaccinate: "V10",
                shotDate: DateTime.Today,
                note: longNote,
                nextShot: DateTime.Today.AddMonths(6)
            );

            IList<ValidationResult> results = ValidateModel(vaccination);

            Assert.Contains(results, result => result.ErrorMessage.Contains("Note's maximum length"));
        }

        [Fact]
        public void Vaccination_WithMinShotDate_ShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Vaccination vaccination = new Vaccination(
                    petId: 1,
                    vaccinate: "Ranger",
                    shotDate: DateTime.MinValue,
                    note: "Aplicado",
                    nextShot: DateTime.Today
                );
            });
        }

        [Fact]
        public void Vaccination_WithInvalidNextShot_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Vaccination vaccination = new Vaccination(
                    petId: 1,
                    vaccinate: "Raiva",
                    shotDate: DateTime.MinValue,
                    note: "Teste",
                    nextShot: DateTime.MinValue
                );
            });
        }
    }
}