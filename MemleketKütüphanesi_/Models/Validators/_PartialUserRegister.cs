using FluentValidation;

namespace MemleketKütüphanesi_.Models.Validators
{
    public class _PartialUserRegister : AbstractValidator<User>
    {
        public _PartialUserRegister() 
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name boş olamaz.")
                .NotEmpty().WithMessage("Name boş olamaz.");

            RuleFor(x => x.Surname)
                .NotNull().WithMessage("Soyisim boş olamaz.")
                .NotEmpty().WithMessage("Soyisim boş olamaz.");

            RuleFor(x => x.TcNo)
                .NotNull().WithMessage("TcNo boş olamaz.")
                .NotEmpty().WithMessage("TcNo boş olamaz.");
        }
    }
}
