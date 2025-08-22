using FluentValidation;
using MediatR;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.Defects.Commands;

public record CreateDefectCommand(int WorkOrderId, string? Severity, string? Description) : IRequest<int>;

public class CreateDefectCommandValidator : AbstractValidator<CreateDefectCommand>
{
    public CreateDefectCommandValidator()
    {
        RuleFor(x => x.WorkOrderId).GreaterThan(0);
    }
}

public class CreateDefectCommandHandler : IRequestHandler<CreateDefectCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateDefectCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateDefectCommand request, CancellationToken cancellationToken)
    {
        var entity = new Defect { WorkOrderId = request.WorkOrderId, Severity = request.Severity, Description = request.Description };
        _context.Defects.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public record UpdateDefectCommand(int Id, int WorkOrderId, string? Severity, string? Description, bool IsClosed) : IRequest;

public class UpdateDefectCommandValidator : AbstractValidator<UpdateDefectCommand>
{
    public UpdateDefectCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.WorkOrderId).GreaterThan(0);
    }
}

public class UpdateDefectCommandHandler : IRequestHandler<UpdateDefectCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateDefectCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateDefectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Defects.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Defect {request.Id} not found");
        }
        entity.WorkOrderId = request.WorkOrderId;
        entity.Severity = request.Severity;
        entity.Description = request.Description;
        entity.IsClosed = request.IsClosed;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record DeleteDefectCommand(int Id) : IRequest;

public class DeleteDefectCommandValidator : AbstractValidator<DeleteDefectCommand>
{
    public DeleteDefectCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class DeleteDefectCommandHandler : IRequestHandler<DeleteDefectCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteDefectCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteDefectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Defects.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _context.Defects.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
