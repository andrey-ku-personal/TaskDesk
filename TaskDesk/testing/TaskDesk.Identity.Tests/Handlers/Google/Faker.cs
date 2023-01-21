using Bogus;
using TaskDesk.Identity.Handlers.Google;
using TaskDesk.Identity.Handlers.Token;
using TaskDesk.Shared.Enums;
using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Tests.Handlers.Google;

public class Faker
{
    public CreateRequest FakeCreateRequest() => new Faker<CreateRequest>()
        .RuleFor(c => c.Id, f => 0)
        .RuleFor(c => c.FirstName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.LastName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
        .Generate();

    public TokenRequest FakeTokenRequest(UserModel model) => new Faker<TokenRequest>()
       .RuleFor(c => c.Type, () => GrandType.Google)
       .RuleFor(c => c.UserIdOrEmail, () => model.Email)
       .Generate();
}
