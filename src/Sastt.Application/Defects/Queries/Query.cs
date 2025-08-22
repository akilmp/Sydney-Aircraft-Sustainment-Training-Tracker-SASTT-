using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.Defects.Queries;

public record GetDefectByIdQuery(int Id) : IRequest<Defect?>;

public class GetDefectByIdQueryHandler : IRequestHandler<GetDefectByIdQuery, Defect?>
{
    private readonly IApplicationDbContext _context;
    public GetDefectByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<Defect?> Handle(GetDefectByIdQuery request, CancellationToken cancellationToken)
        => await _context.Defects.FindAsync(new object[] { request.Id }, cancellationToken);
}

public record GetDefectListQuery() : IRequest<List<Defect>>;

public class GetDefectListQueryHandler : IRequestHandler<GetDefectListQuery, List<Defect>>
{
    private readonly IApplicationDbContext _context;
    public GetDefectListQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<Defect>> Handle(GetDefectListQuery request, CancellationToken cancellationToken)
        => await _context.Defects.ToListAsync(cancellationToken);
}
