using Bogus;
using TaskDesk.Identity.Handlers.Google;

namespace TaskDesk.Identity.Tests.Handlers.Google;

public class Faker
{
    public CreateRequest FakeCreateCommand() => new Faker<CreateRequest>()
        .RuleFor(c => c.Id, f => 0)
        .RuleFor(c => c.FirstName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.LastName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
        .Generate();
}
