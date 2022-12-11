using Bogus;
using TaskDesk.Identity.Handlers.Account;
using TaskDesk.Identity.Handlers.Account.Models;
using TaskDesk.Identity.Handlers.Token;
using TaskDesk.Identity.Tests.Extensions;
using TaskDesk.Shared.Enums;

namespace TaskDesk.Identity.Tests.Handlers.Account;

public class Faker
{
    public CreateRequest FakeCreateRequest() => new Faker<CreateRequest>()
        .RuleFor(c => c.Id, f => 0)
        .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName())
        .RuleFor(c => c.LastName, (f, c) => f.Name.LastName())
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
        .RuleFor(c => c.Password, (f, c) => f.Internet.GeneratePassword(12, 20))
        .Generate();

    public UpdateRequest FakeUpdateRequest(UserModel model) => new Faker<UpdateRequest>()
       .RuleFor(c => c.Id, f => model.Id)
       .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName())
       .RuleFor(c => c.LastName, (f, c) => f.Name.LastName())
       .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
       .RuleFor(c => c.Description, (f, c) => f.Lorem.Sentences(4, ". "))
       .RuleFor(c => c.Website, (f, c) => f.Internet.Url())
       .Generate();

    public TokenRequest FakeTokenRequest(UserModel model, string password) => new Faker<TokenRequest>()
       .RuleFor(c => c.Type, () => GrandType.Password)
       .RuleFor(c => c.UserIdOrEmail, () => model.Email)
       .RuleFor(c => c.Password, () => password)
       .Generate();
}
