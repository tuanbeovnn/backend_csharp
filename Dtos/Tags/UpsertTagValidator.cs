using FluentValidation;

namespace Dtos.Tags;

public class UpsertTagValidator : AbstractValidator<UpsertTag>
{
    public UpsertTagValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}