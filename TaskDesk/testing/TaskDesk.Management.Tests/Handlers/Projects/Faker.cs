using Bogus;
using TaskDesk.Management.Handlers.Project;
using TaskDesk.SharedModel.Enums;
using TaskDesk.SharedModel.Project.Models;

namespace TaskDesk.Management.Tests.Handlers.Projects;

public class Faker
{
    public CreateRequest FakeCreateRequest() => new Faker<CreateRequest>()
        .RuleFor(c => c.Id, f => 0)
        .RuleFor(c => c.Name, (f, c) => f.Company.CompanyName())
        .RuleFor(c => c.Description, (f, c) => f.Lorem.Text().OrNull(f))
        .RuleFor(c => c.Status, (f, c) => f.Random.Enum<ProjectStatus>())
        .Generate();
}
