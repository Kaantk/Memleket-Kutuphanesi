using FluentValidation;
using MemleketKütüphanesi_.Models;

namespace MemleketKütüphanesi_.Validators
{
    public class _UserLoginValidator : AbstractValidator<User>
    {
        public _UserLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("E-mail boş olamaz.")
                .EmailAddress().WithMessage("Lütfen doğru bir e-mail giriniz.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Lütfen bir şifre giriniz.")
                .MaximumLength(15).WithMessage("En fazla 15 karakter girilmelidir.");
        }
    }
}
