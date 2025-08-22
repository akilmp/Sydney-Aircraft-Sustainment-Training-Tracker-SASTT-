using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.WorkOrders.Queries;

public record GetWorkOrderByIdQuery(int Id) : IRequest<WorkOrder?>;

public class GetWorkOrderByIdQueryHandler : IRequestHandler<GetWorkOrderByIdQuery, WorkOrder?>
{
    private readonly IApplicationDbContext _context;
    public GetWorkOrderByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<WorkOrder?> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
        => await _context.WorkOrders.FindAsync(new object[] { request.Id }, cancellationToken);
}

public record GetWorkOrderListQuery() : IRequest<List<WorkOrder>>;

public class GetWorkOrderListQueryHandler : IRequestHandler<GetWorkOrderListQuery, List<WorkOrder>>
{
    private readonly IApplicationDbContext _context;
    public GetWorkOrderListQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<WorkOrder>> Handle(GetWorkOrderListQuery request, CancellationToken cancellationToken)
        => await _context.WorkOrders.ToListAsync(cancellationToken);
}
