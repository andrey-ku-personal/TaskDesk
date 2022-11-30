using AutoMapper.QueryableExtensions;
using TaskDesk.Domain;
using TaskDesk.Identity.Handlers.Account.Models;
using TaskDesk.Shared.Exceptions;
using TaskDesk.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace TaskDesk.Identity.Handlers.Account;

public class GetHandler : IRequestHandler<GetRequest, UserModel>
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

    public async Task<UserModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Users
            .ByQuery(_mapper.Map<GetQuery>(request))
            .ProjectTo<UserModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ??
                throw new NotFoundException($"User/{request.Id} was not found");
    }
}