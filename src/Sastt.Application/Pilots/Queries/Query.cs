using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.Pilots.Queries;

public record GetPilotByIdQuery(int Id) : IRequest<Pilot?>;

public class GetPilotByIdQueryHandler : IRequestHandler<GetPilotByIdQuery, Pilot?>
{
    private readonly IApplicationDbContext _context;
    public GetPilotByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<Pilot?> Handle(GetPilotByIdQuery request, CancellationToken cancellationToken)
        => await _context.Pilots.FindAsync(new object[] { request.Id }, cancellationToken);
}

public record GetPilotListQuery() : IRequest<List<Pilot>>;

public class GetPilotListQueryHandler : IRequestHandler<GetPilotListQuery, List<Pilot>>
{
    private readonly IApplicationDbContext _context;
    public GetPilotListQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<Pilot>> Handle(GetPilotListQuery request, CancellationToken cancellationToken)
        => await _context.Pilots.ToListAsync(cancellationToken);
}
