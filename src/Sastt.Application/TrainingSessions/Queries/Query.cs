using MediatR;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain;

namespace Sastt.Application.TrainingSessions.Queries;

public record GetTrainingSessionByIdQuery(int Id) : IRequest<TrainingSession?>;

public class GetTrainingSessionByIdQueryHandler : IRequestHandler<GetTrainingSessionByIdQuery, TrainingSession?>
{
    private readonly IApplicationDbContext _context;
    public GetTrainingSessionByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<TrainingSession?> Handle(GetTrainingSessionByIdQuery request, CancellationToken cancellationToken)
        => await _context.TrainingSessions.FindAsync(new object[] { request.Id }, cancellationToken);
}

public record GetTrainingSessionListQuery() : IRequest<List<TrainingSession>>;

public class GetTrainingSessionListQueryHandler : IRequestHandler<GetTrainingSessionListQuery, List<TrainingSession>>
{
    private readonly IApplicationDbContext _context;
    public GetTrainingSessionListQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<TrainingSession>> Handle(GetTrainingSessionListQuery request, CancellationToken cancellationToken)
        => await _context.TrainingSessions.ToListAsync(cancellationToken);
}
