using FluentValidation;
using MemleketKütüphanesi_.Models;

namespace MemleketKütüphanesi_.Validators
{
    public class _EmployeeRegisterValidator : AbstractValidator<Employee>
    {
        public _EmployeeRegisterValidator() 
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name boş olamaz.");
 
            RuleFor(x => x.Surname)
                .NotNull().WithMessage("Soyisim boş olamaz.");

            RuleFor(x => x.TcNo)
                .NotNull().WithMessage("TcNo boş olamaz.")
                .MinimumLength(11).WithMessage("Lütfen geçerli bir Tc numarası giriniz.")
                .MaximumLength(11).WithMessage("Lütfen geçerli bir Tc numarası giriniz.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir E-mail giriniz.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Şifre boş olamaz.")
                .MaximumLength(15).WithMessage("Şifreniz 15 karakterden daha uzun olamaz.")
                .MinimumLength(8).WithMessage("Şifreniz 8 karakterden daha az olamaz.");

            RuleFor(x => x.BirthDay)
                .NotNull().WithMessage("Doğum tarihi alanı boş olamaz.");

            RuleFor(x => x.PhoneNumber)
               .NotNull().WithMessage("Telefon numarası boş geçilemez.")
               .MaximumLength(11).WithMessage("Telefon numaranızı kontrol ediniz.")
               .MinimumLength(10).WithMessage("Telefon numaranızı kontrol ediniz.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Adres boş geçilemez.");
        }
    }
}
