
using eFarmCommerce.Model.Requests;
using FluentValidation;

namespace eFarmCommerce.Services.Validators;

public class ProizvodInsertValidator : AbstractValidator<ProizvodInsertRequest>
{
    public ProizvodInsertValidator()
    {
        RuleFor(x => x.Naziv)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Cijena)
            .GreaterThan(0);

        RuleFor(x => x.KolicinaNaStanju)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.KategorijaProizvodaId)
            .GreaterThan(0);

        RuleFor(x => x.ProizvodjacId)
            .GreaterThan(0);
    }
}