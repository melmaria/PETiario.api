
using System.ComponentModel.DataAnnotations;

namespace PETiario.PETiary.Domain.Owners.Entities
{
    public class Owner
    {
        public virtual int Id { get; protected set; }

        [Required]
        [MaxLength(100,
            ErrorMessage = "Name's maximum length is 100 caracteres.")]
        public virtual string Name { get; protected set; }

        [Required]
        [RegularExpression(@"^(\+|00)[1-9]\d{0,2}[\s\-]?\(?\d{1,4}\)?[\s\-]?\d{3,4}[\s\-]?\d{3,4}$")]
        public virtual string Telephone { get; protected set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        [MinLength(10,
            ErrorMessage = "Email needs at least 10 caracteres")]
        public virtual string Email { get; protected set; }

        [MinLength(3,
            ErrorMessage = "Adress needs at least 3 caracteres.")]
        [MaxLength(100,
            ErrorMessage = "Adress's maximum length is 100 caracteres.")]
        public virtual string Adress { get; protected set; }

        protected Owner() {}

        public Owner(string name, string telephone, string email, string adress)
        {
            ChangeName(name);
            ChangeTelephone(telephone);
            ChangeEmail(email);
            ChangeAdress(adress);
        }

        public virtual void ChangeName(string name) => Name = name;
        public virtual void ChangeTelephone(string telephone) => Telephone = telephone;
        public virtual void ChangeEmail(string email) => Email = email;
        public virtual void ChangeAdress(string adress) => Adress = adress;

    }
}