using BlazorServerTest.Core.Models.Categories;
using FluentValidation;

namespace BlazorServerTest.Core.Validators.Categories;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
    }

    private async Task<bool> IsUniqueAsync(string email)
    {
        // Simulates a long running http call
        await Task.Delay(2000);
        return email.ToLower() != "test@test.com";
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateCategoryRequest>.CreateWithOptions((CreateCategoryRequest)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
