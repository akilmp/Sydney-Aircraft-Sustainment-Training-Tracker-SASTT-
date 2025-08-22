using FluentValidation;
using MediatR;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.TrainingSessions.Commands;

public record CreateTrainingSessionCommand(int PilotId, DateTime Date, int Hours) : IRequest<int>;

public class CreateTrainingSessionCommandValidator : AbstractValidator<CreateTrainingSessionCommand>
{
    public CreateTrainingSessionCommandValidator()
    {
        RuleFor(x => x.PilotId).GreaterThan(0);
        RuleFor(x => x.Hours).GreaterThan(0);
    }
}

public class CreateTrainingSessionCommandHandler : IRequestHandler<CreateTrainingSessionCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateTrainingSessionCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateTrainingSessionCommand request, CancellationToken cancellationToken)
    {
        var entity = new TrainingSession { PilotId = request.PilotId, Date = request.Date, Hours = request.Hours };
        _context.TrainingSessions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public record UpdateTrainingSessionCommand(int Id, int PilotId, DateTime Date, int Hours) : IRequest;

public class UpdateTrainingSessionCommandValidator : AbstractValidator<UpdateTrainingSessionCommand>
{
    public UpdateTrainingSessionCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.PilotId).GreaterThan(0);
        RuleFor(x => x.Hours).GreaterThan(0);
    }
}

public class UpdateTrainingSessionCommandHandler : IRequestHandler<UpdateTrainingSessionCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateTrainingSessionCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateTrainingSessionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingSessions.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"TrainingSession {request.Id} not found");
        }
        entity.PilotId = request.PilotId;
        entity.Date = request.Date;
        entity.Hours = request.Hours;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record DeleteTrainingSessionCommand(int Id) : IRequest;

public class DeleteTrainingSessionCommandValidator : AbstractValidator<DeleteTrainingSessionCommand>
{
    public DeleteTrainingSessionCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class DeleteTrainingSessionCommandHandler : IRequestHandler<DeleteTrainingSessionCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteTrainingSessionCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteTrainingSessionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingSessions.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _context.TrainingSessions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
