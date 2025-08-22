using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.Aircraft.Queries;

public record GetAircraftByTailNumberQuery(string TailNumber) : IRequest<Sastt.Domain.Aircraft?>;

public class GetAircraftByTailNumberQueryHandler : IRequestHandler<GetAircraftByTailNumberQuery, Sastt.Domain.Aircraft?>
{
    private readonly IApplicationDbContext _context;
    public GetAircraftByTailNumberQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<Sastt.Domain.Aircraft?> Handle(GetAircraftByTailNumberQuery request, CancellationToken cancellationToken)
        => await _context.Aircraft.FindAsync(new object[] { request.TailNumber }, cancellationToken);
}

public record GetAircraftListQuery() : IRequest<List<Sastt.Domain.Aircraft>>;

public class GetAircraftListQueryHandler : IRequestHandler<GetAircraftListQuery, List<Sastt.Domain.Aircraft>>
{
    private readonly IApplicationDbContext _context;
    public GetAircraftListQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<Sastt.Domain.Aircraft>> Handle(GetAircraftListQuery request, CancellationToken cancellationToken)
        => await _context.Aircraft.ToListAsync(cancellationToken);
}
