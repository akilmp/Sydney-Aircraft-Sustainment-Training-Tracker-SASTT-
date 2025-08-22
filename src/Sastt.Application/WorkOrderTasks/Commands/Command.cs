using FluentValidation;
using MediatR;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.WorkOrderTasks.Commands;

public record CreateWorkOrderTaskCommand(int WorkOrderId, string Description) : IRequest<int>;

public class CreateWorkOrderTaskCommandValidator : AbstractValidator<CreateWorkOrderTaskCommand>
{
    public CreateWorkOrderTaskCommandValidator()
    {
        RuleFor(x => x.WorkOrderId).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class CreateWorkOrderTaskCommandHandler : IRequestHandler<CreateWorkOrderTaskCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateWorkOrderTaskCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateWorkOrderTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new WorkOrderTask { WorkOrderId = request.WorkOrderId, Description = request.Description };
        _context.WorkOrderTasks.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public record UpdateWorkOrderTaskCommand(int Id, string Description, bool IsCompleted) : IRequest;

public class UpdateWorkOrderTaskCommandValidator : AbstractValidator<UpdateWorkOrderTaskCommand>
{
    public UpdateWorkOrderTaskCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class UpdateWorkOrderTaskCommandHandler : IRequestHandler<UpdateWorkOrderTaskCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateWorkOrderTaskCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateWorkOrderTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderTasks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Task {request.Id} not found");
        }
        entity.Description = request.Description;
        entity.IsCompleted = request.IsCompleted;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record DeleteWorkOrderTaskCommand(int Id) : IRequest;

public class DeleteWorkOrderTaskCommandValidator : AbstractValidator<DeleteWorkOrderTaskCommand>
{
    public DeleteWorkOrderTaskCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class DeleteWorkOrderTaskCommandHandler : IRequestHandler<DeleteWorkOrderTaskCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteWorkOrderTaskCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteWorkOrderTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderTasks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _context.WorkOrderTasks.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
