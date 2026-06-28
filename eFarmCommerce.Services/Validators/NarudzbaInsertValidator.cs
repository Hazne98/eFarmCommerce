using eFarmCommerce.Model.Requests;
using FluentValidation;

namespace eFarmCommerce.Services.Validators;

public class NarudzbaInsertValidator : AbstractValidator<NarudzbaInsertRequest>
{
    public NarudzbaInsertValidator()
    {
        RuleFor(x => x.KorisnikId).GreaterThan(0);
        RuleFor(x => x.StatusNarudzbeId).GreaterThan(0);
        RuleFor(x => x.AdresaDostave).NotEmpty().MaximumLength(255);
        RuleFor(x => x.UkupnaCijena).GreaterThanOrEqualTo(0);
    }
}
