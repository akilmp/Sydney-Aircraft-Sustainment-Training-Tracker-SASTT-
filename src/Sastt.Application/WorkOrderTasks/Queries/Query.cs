using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.WorkOrderTasks.Queries;

public record GetWorkOrderTaskByIdQuery(int Id) : IRequest<WorkOrderTask?>;

public class GetWorkOrderTaskByIdQueryHandler : IRequestHandler<GetWorkOrderTaskByIdQuery, WorkOrderTask?>
{
    private readonly IApplicationDbContext _context;
    public GetWorkOrderTaskByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<WorkOrderTask?> Handle(GetWorkOrderTaskByIdQuery request, CancellationToken cancellationToken)
        => await _context.WorkOrderTasks.FindAsync(new object[] { request.Id }, cancellationToken);
}

public record GetWorkOrderTaskListQuery() : IRequest<List<WorkOrderTask>>;

public class GetWorkOrderTaskListQueryHandler : IRequestHandler<GetWorkOrderTaskListQuery, List<WorkOrderTask>>
{
    private readonly IApplicationDbContext _context;
    public GetWorkOrderTaskListQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<WorkOrderTask>> Handle(GetWorkOrderTaskListQuery request, CancellationToken cancellationToken)
        => await _context.WorkOrderTasks.ToListAsync(cancellationToken);
}
