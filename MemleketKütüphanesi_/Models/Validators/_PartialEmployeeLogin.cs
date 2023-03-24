using FluentValidation;

namespace MemleketKütüphanesi_.Models.Validators
{
    public class _PartialEmployeeLogin : AbstractValidator<Employee> // Bu validator sınıfı giriş sayfasında kullanıcının girdiği verileri denetleyecek.
    {
        public _PartialEmployeeLogin()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("E-mail boş olamaz.")
                .NotEmpty().WithMessage("E-mail boş olamaz.")
                .EmailAddress().WithMessage("Lütfen doğru bir e-mail giriniz.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Lütfen bir şifre giriniz.")
                .NotEmpty().WithMessage("Lütfen bir şifre giriniz.")
                .MaximumLength(15).WithMessage("En fazla 15 karakter girilmelidir.");        }
    }
}
