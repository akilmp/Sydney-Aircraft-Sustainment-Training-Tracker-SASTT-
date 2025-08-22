using FluentValidation;
using MediatR;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.Aircraft.Commands;

public record CreateAircraftCommand(string TailNumber, string Base) : IRequest<string>;

public class CreateAircraftCommandValidator : AbstractValidator<CreateAircraftCommand>
{
    public CreateAircraftCommandValidator()
    {
        RuleFor(x => x.TailNumber).NotEmpty();
        RuleFor(x => x.Base).NotEmpty();
    }
}

public class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, string>
{
    private readonly IApplicationDbContext _context;
    public CreateAircraftCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<string> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
    {
        var entity = new Sastt.Domain.Aircraft { TailNumber = request.TailNumber, Base = request.Base };
        _context.Aircraft.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.TailNumber;
    }
}

public record UpdateAircraftCommand(string TailNumber, string Base) : IRequest;

public class UpdateAircraftCommandValidator : AbstractValidator<UpdateAircraftCommand>
{
    public UpdateAircraftCommandValidator()
    {
        RuleFor(x => x.TailNumber).NotEmpty();
        RuleFor(x => x.Base).NotEmpty();
    }
}

public class UpdateAircraftCommandHandler : IRequestHandler<UpdateAircraftCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateAircraftCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateAircraftCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Aircraft.FindAsync(new object[] { request.TailNumber }, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Aircraft {request.TailNumber} not found");
        }
        entity.Base = request.Base;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public record DeleteAircraftCommand(string TailNumber) : IRequest;

public class DeleteAircraftCommandValidator : AbstractValidator<DeleteAircraftCommand>
{
    public DeleteAircraftCommandValidator()
    {
        RuleFor(x => x.TailNumber).NotEmpty();
    }
}

public class DeleteAircraftCommandHandler : IRequestHandler<DeleteAircraftCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteAircraftCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteAircraftCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Aircraft.FindAsync(new object[] { request.TailNumber }, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _context.Aircraft.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
