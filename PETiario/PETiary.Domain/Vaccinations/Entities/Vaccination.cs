
using System.ComponentModel.DataAnnotations;

namespace PETiario.PETiary.Domain.Vaccinations.Entities
{
    public class Vaccination
    {
        public virtual int Id { get; protected set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pet can not be null.")]
        public virtual int PetId { get; protected set; }

        [Required]
        [MaxLength(100,
            ErrorMessage = "Vaccinate's maximum length is 100 caracteres.")]
        public virtual string Vaccinate { get; protected set; }

        [Required]
        public virtual DateTime ShotDate { get; protected set; }
        public virtual DateTime? NextShot { get; protected set; }
        public virtual bool Applied { get; protected set; } = false;

        [MaxLength(100,
            ErrorMessage = "Note's maximum length is 100 caracteres.")]
        [MinLength(3,
            ErrorMessage = "Note's minimum length is 3 caracteres.")]
        public virtual string Note { get; protected set; }

        protected Vaccination() { }

        public Vaccination(int petId, string vaccinate, DateTime shotDate, string note, DateTime? nextShot, bool applied = false)
        {
            ChangePetId(petId);
            ChangeVaccinate(vaccinate);
            ChangeShotDate(shotDate);
            ChangeNextShot(nextShot);
            ChangeApplied(applied);
            ChangeNote(note);  
        }

        public virtual void ChangeNote(string note) => Note = note;
        public virtual void ChangeApplied(bool applied) => Applied = applied;
        public virtual void ChangeNextShot(DateTime? nextShot)
        {
            if(nextShot == ShotDate && nextShot == DateTime.MinValue) throw new ArgumentException("Invalid date.");
            NextShot = nextShot;
        }
        public virtual void ChangeShotDate(DateTime shotDate)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(shotDate, DateTime.MinValue);
            ShotDate = shotDate;
        } 
        public virtual void ChangeVaccinate(string vaccinate) => Vaccinate = vaccinate;
        public virtual void ChangePetId(int petId) => PetId = petId;
        
    }
}