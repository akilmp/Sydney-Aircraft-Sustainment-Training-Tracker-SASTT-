using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;
using Sastt.Domain.Enums;

namespace Sastt.Application.WorkOrders.Commands;

public record CreateWorkOrderCommand(string Title, int AircraftId) : IRequest<int>;

public class CreateWorkOrderCommandValidator : AbstractValidator<CreateWorkOrderCommand>
{
    public CreateWorkOrderCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}

public class CreateWorkOrderCommandHandler : IRequestHandler<CreateWorkOrderCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateWorkOrderCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = new WorkOrder { Title = request.Title, AircraftId = request.AircraftId };
        _context.WorkOrders.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public record UpdateWorkOrderCommand(int Id, string Title, int AircraftId) : IRequest;

public class UpdateWorkOrderCommandValidator : AbstractValidator<UpdateWorkOrderCommand>
{
    public UpdateWorkOrderCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty();
    }
}

public class UpdateWorkOrderCommandHandler : IRequestHandler<UpdateWorkOrderCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateWorkOrderCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrders.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"WorkOrder {request.Id} not found");
        }
        entity.Title = request.Title;
        entity.AircraftId = request.AircraftId;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record DeleteWorkOrderCommand(int Id) : IRequest;

public class DeleteWorkOrderCommandValidator : AbstractValidator<DeleteWorkOrderCommand>
{
    public DeleteWorkOrderCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class DeleteWorkOrderCommandHandler : IRequestHandler<DeleteWorkOrderCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteWorkOrderCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrders.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _context.WorkOrders.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record TransitionWorkOrderStatusCommand(int Id, WorkOrderStatus NewStatus) : IRequest;

public class TransitionWorkOrderStatusCommandValidator : AbstractValidator<TransitionWorkOrderStatusCommand>
{
    public TransitionWorkOrderStatusCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class TransitionWorkOrderStatusCommandHandler : IRequestHandler<TransitionWorkOrderStatusCommand>
{
    private readonly IApplicationDbContext _context;
    public TransitionWorkOrderStatusCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(TransitionWorkOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrders
            .Include(w => w.Tasks)
            .Include(w => w.Defects)
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"WorkOrder {request.Id} not found");
        }

        bool allowed = entity.Status switch
        {
            WorkOrderStatus.Open => request.NewStatus is WorkOrderStatus.InProgress or WorkOrderStatus.Cancelled,
            WorkOrderStatus.InProgress => request.NewStatus is WorkOrderStatus.QaReview or WorkOrderStatus.Cancelled,
            WorkOrderStatus.QaReview => request.NewStatus is WorkOrderStatus.Completed or WorkOrderStatus.Deferred,
            WorkOrderStatus.Deferred => request.NewStatus is WorkOrderStatus.InProgress or WorkOrderStatus.Cancelled,
            _ => false
        };

        if (!allowed)
        {
            throw new InvalidOperationException($"Cannot transition from {entity.Status} to {request.NewStatus}");
        }

        if (request.NewStatus == WorkOrderStatus.Completed)
        {
            if (entity.Tasks.Any(t => !t.IsCompleted))
            {
                throw new InvalidOperationException("Cannot close work order with open tasks.");
            }
            if (entity.Defects.Any(d => !d.IsClosed && string.Equals(d.Severity, "High", StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Cannot close work order with high severity defects.");
            }
        }

        entity.Status = request.NewStatus;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
