using AutoMapper.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Domain;
using TaskDesk.Domain.Entities;
using TaskDesk.Identity.Handlers.Account;
using TaskDesk.Identity.Handlers.Account.Models;
using TaskDesk.Shared.Exceptions;
using TaskDesk.Shared.Queries;

namespace Desk.Identity.Services;

public class UserStore : IUserPasswordStore<UserModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserStore(
        IDbContextFactory<EntitiesDbContext> contextFactory, 
        IMapper mapper, 
        IMediator mediator
    )
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _mediator = mediator;
    }

    public Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return CreateInternalAsync(user, cancellationToken);
    }

    private async Task<IdentityResult> CreateInternalAsync(UserModel user, CancellationToken cancellationToken)
    {
        return await UpdateInternalAsync(user, cancellationToken);
    }

    public Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return DeleteInternalAsync(user, cancellationToken);
    }

    private async Task<IdentityResult> DeleteInternalAsync(UserModel user, CancellationToken cancellationToken)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync();

        await db.Users
            .Persist(_mapper)
            .RemoveAsync(user, cancellationToken);

        await db.CommitTransactionAsync();

        return IdentityResult.Success;
    }

    public Task<UserModel?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (userId == null) throw new ArgumentNullException(nameof(userId));

        return FindByIdInternalAsync(userId, cancellationToken);
    }

    private async Task<UserModel?> FindByIdInternalAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            return await _mediator.Send(new GetRequest { UserIdOrEmail = userId }, cancellationToken);
        }
        catch (NotFoundException)
        {
            return null!;
        }
    }

    public Task<UserModel?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return GetUserIdInternalAsync(user, cancellationToken);
    }

    private async Task<string> GetUserIdInternalAsync(UserModel user, CancellationToken cancellationToken)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .AsNoTracking()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserId}' was not found");

        return entity.UserId;
    }

    public Task<string?> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetUserNameAsync(UserModel user, string? userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(UserModel user, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return UpdateInternalAsync(user, cancellationToken);
    }

    public async Task<IdentityResult> UpdateInternalAsync(UserModel user, CancellationToken cancellationToken)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync();

        await db.Users
            .Persist(_mapper)
            .InsertOrUpdateAsync(user, cancellationToken);

        await db.CommitTransactionAsync();

        return IdentityResult.Success;
    }

    #region User Password Store

    public Task<string?> GetPasswordHashAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return GetPasswordHashInternalAsync(user, cancellationToken);
    }

    private async Task<string?> GetPasswordHashInternalAsync(UserModel user, CancellationToken cancellationToken)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .AsNoTracking()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserId}' was not found");

        return entity.PasswordHash;
    }

    public Task<bool> HasPasswordAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return HasPasswordInternalAsync(user, cancellationToken);
    }

    public async Task<bool> HasPasswordInternalAsync(UserModel user, CancellationToken cancellationToken)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserId}' was not found");

        return !string.IsNullOrWhiteSpace(entity.PasswordHash);
    }

    public Task SetPasswordHashAsync(UserModel user, string? passwordHash, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentNullException(nameof(user));

        return SetPasswordHashInternalAsync(user, passwordHash!, cancellationToken);
    }

    private async Task SetPasswordHashInternalAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserId}' was not found");

        entity.PasswordHash = passwordHash;

        await db.CommitTransactionAsync();
    }

    #endregion

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        //cleanup
    }
}