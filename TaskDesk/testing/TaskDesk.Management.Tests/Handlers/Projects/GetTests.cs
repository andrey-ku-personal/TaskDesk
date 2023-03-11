using Shouldly;
using TaskDesk.Management.Handlers.Project;
using TaskDesk.Management.Tests.Fixtures;
using TaskDesk.Shared.Exceptions;
using TaskDesk.SharedModel.Handlers.Project.Models;

namespace TaskDesk.Management.Tests.Handlers.Projects;

[Collection(nameof(SliceFixture))]
public class GetTests
{
    private readonly SliceFixture _fixture;

    public GetTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Can_Get_Project()
    {
        var created = await _fixture.SendAsync(new Faker().FakeCreateRequest());

        var result = await _fixture.SendAsync(new GetRequest { Id = created.Id });

        result.AsSource()
            .OfLikeness<ProjectModel>()
            .Without(x => x.Id)
            .ShouldEqual(created);
    }

    [Fact]
    public async Task Validation_exception_When_Id_Zero()
    {
        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new GetRequest()));
    }
}
