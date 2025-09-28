
using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.GroomingBaths.Enumerators;

namespace PETiario.PETiary.Domain.GroomingBaths.Entities
{
    public class GroomingBath
    {
        public virtual int Id { get; protected set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pet can not be null.")]
        public virtual int PetId { get; protected set; }

        [Required]
        public virtual DateTime Date { get; protected set; }

        [Required]
        public virtual KindEnum Kind { get; protected set; }

        [MaxLength(100,
            ErrorMessage = "Location's maximum length is 100 caracteres.")]
        [MinLength(3,
            ErrorMessage = "Location's minimum length is 3 caracteres.")]
        public virtual string Location { get; protected set; }

        [MaxLength(100,
            ErrorMessage = "Note's maximum length is 100 caracteres.")]
        [MinLength(3,
            ErrorMessage = "Note's minimum length is 3 caracteres.")]
        public virtual string Note { get; protected set; }

        protected GroomingBath() { }

        public GroomingBath(int petId, DateTime date, KindEnum kind, string location, string note)
        {
            ChangePetId(petId);
            ChangeDate(date);
            ChangeKind(kind);
            ChangeLocation(location);
            ChangeNote(note);
        }
        public virtual void ChangeNote(string note) => Note = note;
        public virtual void ChangeLocation(string location) => Location = location;
        public virtual void ChangeKind(KindEnum kind) => Kind = kind;
        public virtual void ChangeDate(DateTime date)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(date, DateTime.MinValue);
            Date = date; 
        }
        public virtual void ChangePetId(int petId) => PetId = petId;
    }
}