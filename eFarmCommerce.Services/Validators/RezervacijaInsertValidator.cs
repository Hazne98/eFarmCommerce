using eFarmCommerce.Model.Requests;
using FluentValidation;

namespace eFarmCommerce.Services.Validators;

public class RezervacijaInsertValidator : AbstractValidator<RezervacijaInsertRequest>
{
    public RezervacijaInsertValidator()
    {
        RuleFor(x => x.KorisnikId).GreaterThan(0);
        RuleFor(x => x.StatusRezervacijeId).GreaterThan(0);
        RuleFor(x => x.DatumPocetka).LessThan(x => x.DatumZavrsetka);
        RuleFor(x => x.UkupnaCijena).GreaterThanOrEqualTo(0);
    }
}
