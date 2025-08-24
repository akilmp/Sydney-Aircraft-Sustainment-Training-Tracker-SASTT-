using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using TaskEntity = Sastt.Domain.Entities.Task;

namespace Sastt.Application.WorkOrderTasks.Queries;

public record GetTaskByIdQuery(Guid Id) : IRequest<TaskEntity?>;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskEntity?>
{
    private readonly IApplicationDbContext _context;
    public GetTaskByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<TaskEntity?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        => await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);
}

public record GetTaskListQuery() : IRequest<List<TaskEntity>>;

public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, List<TaskEntity>>
{
    private readonly IApplicationDbContext _context;
    public GetTaskListQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<TaskEntity>> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        => await _context.Tasks.ToListAsync(cancellationToken);
}
