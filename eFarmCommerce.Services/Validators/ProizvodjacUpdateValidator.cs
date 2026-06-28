using eFarmCommerce.Model.Requests;
using FluentValidation;

namespace eFarmCommerce.Services.Validators;

public class ProizvodjacUpdateValidator : AbstractValidator<ProizvodjacUpdateRequest>
{
    public ProizvodjacUpdateValidator()
    {
        RuleFor(x => x.Naziv).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
        RuleFor(x => x.GradId).GreaterThan(0);
    }
}
