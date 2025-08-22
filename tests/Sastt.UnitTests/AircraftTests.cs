using System;
using Sastt.Domain;
using Xunit;

namespace Sastt.UnitTests;

public class AircraftTests
{
    [Fact]
    public void AdvanceStatus_ProgressesThroughLifecycle()
    {
        var aircraft = new Aircraft { TailNumber = "SY-001", Base = "YSSY" };

        aircraft.AdvanceStatus(); // Draft -> Planned
        aircraft.AdvanceStatus(); // Planned -> InProgress
        aircraft.AdvanceStatus(); // InProgress -> QaReview
        aircraft.AdvanceStatus(); // QaReview -> Closed

        Assert.Equal(AircraftStatus.Closed, aircraft.Status);
    }

    [Fact]
    public void AdvanceStatus_Throws_WhenAlreadyClosed()
    {
        var aircraft = new Aircraft { TailNumber = "SY-001", Base = "YSSY" };

        aircraft.AdvanceStatus();
        aircraft.AdvanceStatus();
        aircraft.AdvanceStatus();
        aircraft.AdvanceStatus();

        Assert.Throws<InvalidOperationException>(() => aircraft.AdvanceStatus());
    }
}
