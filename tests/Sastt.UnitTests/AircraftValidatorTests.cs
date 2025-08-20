using FluentValidation.TestHelper;
using Sastt.Domain;
using Sastt.Domain.Validators;
using Xunit;

namespace Sastt.UnitTests;

public class AircraftValidatorTests
{
    private readonly AircraftValidator _validator = new();

    [Fact]
    public void TailNumber_is_required()
    {
        var model = new Aircraft { TailNumber = "", Base = "YSSY" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.TailNumber);
    }

    [Theory]
    [InlineData("YSSY")]
    [InlineData("YWLM")]
    public void Base_is_valid_when_allowed(string baseCode)
    {
        var model = new Aircraft { TailNumber = "SY-001", Base = baseCode };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Base);
    }

    [Fact]
    public void Base_is_invalid_when_unknown()
    {
        var model = new Aircraft { TailNumber = "SY-001", Base = "UNKNOWN" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Base);
    }

    [Fact]
    public void AdvanceStatus_moves_to_next_phase()
    {
        var aircraft = new Aircraft { TailNumber = "SY-001", Base = "YSSY" };
        aircraft.AdvanceStatus();
        Assert.Equal(AircraftStatus.Planned, aircraft.Status);
    }
}
