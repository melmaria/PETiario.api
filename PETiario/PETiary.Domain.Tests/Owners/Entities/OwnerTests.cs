using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.Owners.Entities;
using Xunit;

namespace PETiario.PETiary.Domain.Tests.Owners.Entities
{
    public class OwnerTests
    {
        private static List<ValidationResult> ValidateModel(object model)
        {
            List<ValidationResult> results = [];
            ValidationContext context = new(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, context, results, validateAllProperties: true);
            return results;
        }

        [Fact]
        public void Owner_WithValidData_ShouldBeValid()
        {
            Owner owner = new(
                name: "Joao da silva",
                telephone: "+351 912 345 679",
                email: "joao@example.com",
                adress: "Prince street, 123"
            );

            var results = ValidateModel(owner);

            Assert.Empty(results);
        }

        [Fact]
        public void Owner_WithInvalidEmail_ShouldFailValidation()
        {
            Owner owner = new(
                name: "Joao da silva",
                telephone: "+351 912 345 678",
                email: "invalid_email",
                adress: "Prince street, 123"
            );

            var results = ValidateModel(owner);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Email is invalid."));
        }

        [Fact]
        public void Owner_WithShortName_ShouldFailValidation()
        {
            Owner owner = new(
                name: "Jo",
                telephone: "+351 958 654 258",
                email: "joao@example.com",
                adress: "X street"
            );

            var results = ValidateModel(owner);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Name can not be null."));
        }

        [Fact]
        public void Owner_WithInvalidPhone_ShouldFailValidation()
        {
            Owner owner = new(
                name: "Joao",
                telephone: "123",
                email: "joao@example.com",
                adress: "X street"
            );

            var results = ValidateModel(owner);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Telephone number is invalid."));
        }

    }
}