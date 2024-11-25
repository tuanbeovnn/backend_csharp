using FluentValidation;

namespace Dtos.Tags;

public class UpsertTagValidator : AbstractValidator<UpsertTag>
{
    public UpsertTagValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(f => f.Computers);


        RuleFor(f => f.Computers).Custom((x, y) =>
        {

        });
    }
}