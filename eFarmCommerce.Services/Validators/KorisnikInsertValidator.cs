using eFarmCommerce.Model.Requests;
using FluentValidation;

namespace eFarmCommerce.Services.Validators;

public class KorisnikInsertValidator : AbstractValidator<KorisnikInsertRequest>
{
    public KorisnikInsertValidator()
    {
        RuleFor(x => x.Ime).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Prezime).NotEmpty().MaximumLength(100);
        RuleFor(x => x.KorisnickoIme).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(150);
        RuleFor(x => x.Lozinka).NotEmpty().MinimumLength(6);
        RuleFor(x => x.GradId).GreaterThan(0);
        RuleFor(x => x.UlogaId).GreaterThan(0);
    }
}
