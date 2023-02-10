using BlazorServerTest.ViewModels;
using FluentValidation;

namespace BlazorServerTest.Validators;
public class ChangeRecipeValidator : AbstractValidator<ChangeRecipeViewModel>
{
    public ChangeRecipeValidator()
    {
        RuleFor(address => address.Name).NotNull().NotEmpty().Length(1, 250); //.WithMessage("");
    }
}
