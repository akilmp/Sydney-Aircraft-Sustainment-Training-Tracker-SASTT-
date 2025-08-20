using FluentValidation;
using System.Linq;

namespace Sastt.Domain.Validators;

public class AircraftValidator : AbstractValidator<Aircraft>
{
    public static readonly string[] AllowedBases = new[] { "YSSY", "YWLM" };

    public AircraftValidator()
    {
        RuleFor(x => x.TailNumber).NotEmpty();
        RuleFor(x => x.Base)
            .Must(baseCode => AllowedBases.Contains(baseCode))
            .WithMessage("Base must be YSSY or YWLM");
    }
}
