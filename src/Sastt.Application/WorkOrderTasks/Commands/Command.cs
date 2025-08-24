using FluentValidation;
using MediatR;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain.Enums;
using TaskEntity = Sastt.Domain.Entities.Task;

namespace Sastt.Application.WorkOrderTasks.Commands;

public record CreateTaskCommand(Guid WorkOrderId, string Title, string Description, DateTime? DueDate) : IRequest<Guid>;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.WorkOrderId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateTaskCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new TaskEntity
        {
            WorkOrderId = request.WorkOrderId,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate
        };
        _context.Tasks.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public record UpdateTaskCommand(Guid Id, string Title, string Description, TaskStatus Status, DateTime? DueDate) : IRequest;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateTaskCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Task {request.Id} not found");
        }
        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Status = request.Status;
        entity.DueDate = request.DueDate;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record DeleteTaskCommand(Guid Id) : IRequest;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteTaskCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _context.Tasks.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
