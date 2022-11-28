using AutoMapper;
using AutoMapper.QueryableExtensions;
using TaskDesk.Domain;
using TaskDesk.Identity.Handlers.User.Models;
using TaskDesk.Shared.Exceptions;
using TaskDesk.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaskDesk.Identity.Handlers.User;

public class GetHandler : IRequestHandler<GetCommand, UserModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<UserModel> Handle(GetCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Users
            .ByQuery(_mapper.Map<GetQuery>(command))
            .ProjectTo<UserModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ??
                throw new NotFoundException($"User/{command.Id} was not found");
    }
}