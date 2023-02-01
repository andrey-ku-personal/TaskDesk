using Bogus;
using TaskDesk.Management.Handlers.Project;

namespace TaskDesk.Management.Tests.Handlers.Projects;

public class Faker
{
    public CreateRequest FakeCreateRequest() => new Faker<CreateRequest>()
        .RuleFor(c => c.Id, f => 0)
        .RuleFor(c => c.Name, (f, c) => f.Company.CompanyName())
        .RuleFor(c => c.Description, (f, c) => f.Lorem.Text().OrNull(f))
        .Generate();
}
