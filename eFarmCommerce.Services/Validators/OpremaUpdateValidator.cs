using eFarmCommerce.Model.Requests;
using FluentValidation;

namespace eFarmCommerce.Services.Validators;

public class OpremaUpdateValidator : AbstractValidator<OpremaUpdateRequest>
{
    public OpremaUpdateValidator()
    {
        RuleFor(x => x.Naziv).NotEmpty().MaximumLength(150);
        RuleFor(x => x.CijenaPoDanu).GreaterThan(0);
        RuleFor(x => x.KategorijaOpremeId).GreaterThan(0);
        RuleFor(x => x.ProizvodjacId).GreaterThan(0);
    }
}
