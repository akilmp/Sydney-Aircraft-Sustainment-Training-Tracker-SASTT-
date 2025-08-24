using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain.Entities;

namespace Sastt.Application.Pilots;

/// <summary>
/// Provides helper methods for maintaining pilot currency records.
/// </summary>
public class PilotCurrencyService
{
    private readonly IApplicationDbContext _context;
    private const int CurrencyWindowDays = 60;

    public PilotCurrencyService(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Updates the pilot's currency expiration based on a completed training session.
    /// Creates a record if one does not already exist.
    /// </summary>
    public async Task UpdateCurrencyAsync(Guid pilotId, DateTime completionDate, CancellationToken cancellationToken = default)
    {
        var expiration = completionDate.AddDays(CurrencyWindowDays);

        var currency = await _context.PilotCurrencies
            .FirstOrDefaultAsync(pc => pc.PilotId == pilotId, cancellationToken);

        if (currency is null)
        {
            currency = new PilotCurrency
            {
                PilotId = pilotId,
                CurrencyType = "GENERAL",
                ExpirationDate = expiration
            };
            _context.PilotCurrencies.Add(currency);
        }
        else
        {
            currency.ExpirationDate = expiration;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}

