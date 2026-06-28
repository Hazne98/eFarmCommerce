using eFarmCommerce.Model.Requests;
using FluentValidation;

namespace eFarmCommerce.Services.Validators;

public class ProizvodjacInsertValidator : AbstractValidator<ProizvodjacInsertRequest>
{
    public ProizvodjacInsertValidator()
    {
        RuleFor(x => x.Naziv).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
        RuleFor(x => x.GradId).GreaterThan(0);
    }
}
