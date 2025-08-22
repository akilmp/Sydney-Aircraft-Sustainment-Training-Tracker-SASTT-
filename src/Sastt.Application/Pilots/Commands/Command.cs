using FluentValidation;
using MediatR;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.Pilots.Commands;

public record CreatePilotCommand(string Name) : IRequest<int>;

public class CreatePilotCommandValidator : AbstractValidator<CreatePilotCommand>
{
    public CreatePilotCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreatePilotCommandHandler : IRequestHandler<CreatePilotCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreatePilotCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreatePilotCommand request, CancellationToken cancellationToken)
    {
        var entity = new Pilot { Name = request.Name };
        _context.Pilots.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public record UpdatePilotCommand(int Id, string Name) : IRequest;

public class UpdatePilotCommandValidator : AbstractValidator<UpdatePilotCommand>
{
    public UpdatePilotCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdatePilotCommandHandler : IRequestHandler<UpdatePilotCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdatePilotCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdatePilotCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Pilots.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Pilot {request.Id} not found");
        }
        entity.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record DeletePilotCommand(int Id) : IRequest;

public class DeletePilotCommandValidator : AbstractValidator<DeletePilotCommand>
{
    public DeletePilotCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class DeletePilotCommandHandler : IRequestHandler<DeletePilotCommand>
{
    private readonly IApplicationDbContext _context;
    public DeletePilotCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeletePilotCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Pilots.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _context.Pilots.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
