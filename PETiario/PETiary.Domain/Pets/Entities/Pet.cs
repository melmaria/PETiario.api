
using System.ComponentModel.DataAnnotations;
using PETiario.PETiary.Domain.Pets.Enumerators;

namespace PETiario.PETiary.Domain.Pets.Entities
{
    public class Pet
    {
        public virtual int Id { get; protected set; }

        [Required]
        [MaxLength(100,
            ErrorMessage = "Name's maximum length is 100 caracteres.")]
        public virtual string Name { get; protected set; }

        [Required]
        public virtual SpeciesEnum Species { get; protected set; }

        [MaxLength(100,
            ErrorMessage = "Breed's maximum length is 100 caracteres.")]
        [MinLength(1,
            ErrorMessage = "Breed can not be null.")]
        public virtual string Breed { get; protected set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Age must be greater than 0.")]
        public virtual double Age { get; protected set; }

        [Required]
        public virtual GenderEnum Gender { get; protected set; }
        public virtual string Picture { get; protected set; }
        public virtual int IdOwner { get; protected set; }

        protected Pet() { }

        public Pet(string name, SpeciesEnum species, string breed, double age, GenderEnum gender, string picture, int idOwner)
        {
            ChangeName(name);
            ChangeSpecies(species);
            ChangeBreed(breed);
            ChangeAge(age);
            ChangeGender(gender);
            ChangePicture(picture);
            ChangeIdOwner(idOwner);
        }

        public virtual void ChangeIdOwner(int idOwner) => IdOwner = idOwner;
        public virtual void ChangePicture(string picture) => Picture = picture;
        public virtual void ChangeGender(GenderEnum gender) => Gender = gender;
        public virtual void ChangeAge(double age) => Age = age;
        public virtual void ChangeBreed(string breed) => Breed = breed;
        public virtual void ChangeSpecies(SpeciesEnum species) => Species = species;
        public virtual void ChangeName(string name) => Name = name;
        
    }
}