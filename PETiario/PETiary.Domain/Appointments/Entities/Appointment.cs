
using System.ComponentModel.DataAnnotations;

namespace PETiario.PETiary.Domain.Appointments.Entities
{
    public class Appointment
    {
        public virtual int Id { get; protected set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pet can not be null.")]
        public virtual int PetId { get; protected set; }

        [Required]
        public virtual DateTime Date { get; protected set; }

        [Required]
        [MaxLength(100,
            ErrorMessage = "Description's maximum length is 100 caracteres.")]
        public virtual string Description { get; protected set; }

        [MaxLength(100,
            ErrorMessage = "Vet's maximum length is 100 caracteres.")]
        [MinLength(3,
            ErrorMessage = "Vet's minimum length is 3 caracteres.")]
        public virtual string Vet { get; protected set; }

        [MaxLength(100,
            ErrorMessage = "Note's maximum length is 100 caracteres.")]
        [MinLength(3,
            ErrorMessage = "Note's minimum length is 3 caracteres.")]
        public virtual string Note { get; protected set; }
        public virtual bool Attend { get; protected set; } = false;

        protected Appointment() { }

        public Appointment(
            int petId, DateTime date, string description, string vet, string note, bool attend = false)
        {
            ChangePetId(petId);
            ChangeDate(date);
            ChangeDescription(description);
            ChangeVet(vet);
            ChangeNote(note);
            ChangeAttend(attend);
        }

        public virtual void ChangeAttend(bool attend) => Attend = attend;
        public virtual void ChangeNote(string note) => Note = note;
        public virtual void ChangeVet(string vet) => Vet = vet;
        public virtual void ChangeDescription(string description) => Description = description;
        public virtual void ChangeDate(DateTime date)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(date, DateTime.MinValue);
            Date = date;
        }
        public virtual void ChangePetId(int petId) => PetId = petId;
    }
}