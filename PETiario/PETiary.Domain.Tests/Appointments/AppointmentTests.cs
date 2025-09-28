
using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.Appointments.Entities;
using Xunit;

namespace PETiario.PETiary.Domain.Tests.Appointments
{
    public class AppointmentTests
    {
        private static List<ValidationResult> ValidateModel(object model)
        {
            List<ValidationResult> results = [];
            ValidationContext context = new (model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Appointment_WithValidData_ShouldBeValid()
        {
            Appointment appointment = new (
                petId: 1,
                date: DateTime.Now.AddDays(1),
                description: "Rotine appointment",
                vet: "Dr. Carlos",
                note: "Ok",
                attend: true
            );

            var results = ValidateModel(appointment);

            Assert.Empty(results);
        }

        [Fact]
        public void Appointment_WithMissingPetId_ShouldFailValidation()
        {
            Appointment appointment = new (
                petId: 0, 
                date: DateTime.Now,
                description: "Vaccination",
                vet: "Dr. Ana",
                note: "Ok"
            );

            var results = ValidateModel(appointment);

            Assert.Empty(results);
        }

        [Fact]
        public void Appointment_WithMissingDescription_ShouldFailValidation()
        {
            Appointment appointment = new (
                petId: 1,
                date: DateTime.Now,
                description: null,
                vet: "Dr. Ana",
                note: "Note"
            );

            var results = ValidateModel(appointment);

            Assert.Contains(results, r => r.MemberNames.Contains("Description"));
        }

        [Fact]
        public void Appointment_WithTooLongDescription_ShouldFailValidation()
        {
            var longDescription = new string('a', 101);

            Appointment appointment = new (
                petId: 1,
                date: DateTime.Now,
                description: longDescription,
                vet: "Dr. Ana",
                note: "Note"
            );

            var results = ValidateModel(appointment);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Description's maximum length"));
        }

        [Fact]
        public void Appointment_WithShortVet_ShouldFailValidation()
        {
            Appointment appointment = new (
                petId: 1,
                date: DateTime.Now,
                description: "Appointment",
                vet: "Dr",  
                note: "Note"
            );

            var results = ValidateModel(appointment);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Vet's minimum length"));
        }

        [Fact]
        public void Appointment_WithShortNote_ShouldFailValidation()
        {
            Appointment appointment = new(
                petId: 1,
                date: DateTime.Now,
                description: "Appointment",
                vet: "Dr. Ana",
                note: "Oi"
            );

            var results = ValidateModel(appointment);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Note's minimum length"));
        }
        
        [Fact]
        public void Appointment_WithMinDate_ShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Appointment appointment = new Appointment(
                    petId: 1,
                    date: DateTime.MinValue,
                    description: "Appointment",
                    vet: "Dr. Silva",
                    note: "ok"
                );
            });
        }
    }
}