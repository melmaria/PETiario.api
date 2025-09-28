
using System.ComponentModel.DataAnnotations;

namespace PETiario.PETiary.Domain.Walks.Entities
{
    public class Walk
    {
        public virtual int Id { get; protected set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pet can not be null.")]
        public virtual int PetId { get; protected set; }
        public virtual DateTime Date { get; protected set; }
        public virtual int DurationMinutes { get; protected set; }
        [Required]
        [MaxLength(100,
            ErrorMessage = "Responsible's maximum length is 100 caracteres.")]
        public virtual string Responsible { get; protected set; }

        [MaxLength(100,
            ErrorMessage = "Note's maximum length is 100 caracteres.")]
        [MinLength(3,
            ErrorMessage = "Note's minimum length is 3 caracteres.")]
        public virtual string Note { get; protected set; }

        protected Walk() { }

        public Walk(int petId, DateTime date, int durationMinutes, string note, string responsible)
        {
            ChangePetId(petId);
            ChangeDate(date);
            ChangeDurationMinutes(durationMinutes);
            ChangeNote(note);
            ChangeResponsible(responsible);
        }

        public virtual void ChangeResponsible(string responsible) => Responsible = responsible;
        public virtual void ChangeNote(string note) => Note = note;
        public virtual void ChangeDurationMinutes(int durationMinutes) => DurationMinutes = durationMinutes;
        public virtual void ChangeDate(DateTime date)
        {
            if(date != DateTime.MinValue)
                Date = date;
        }
        public virtual void ChangePetId(int petId) => PetId = petId;
    }
}