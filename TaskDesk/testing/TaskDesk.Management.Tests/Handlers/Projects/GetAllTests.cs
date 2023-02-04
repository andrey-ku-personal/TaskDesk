using Shouldly;
using TaskDesk.Management.Handlers.Project;
using TaskDesk.Management.Tests.Fixtures;

namespace TaskDesk.Management.Tests.Handlers.Projects;

[Collection(nameof(SliceFixture))]
public class GetAllTests
{
    private readonly SliceFixture _fixture;

    public GetAllTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Can_Create_Project()
    {
        var createRequest = new Faker().FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var result = await _fixture.SendAsync(new GetAllRequest() { PageSize = int.MaxValue });

        result.TotalCount.ShouldBeGreaterThan(0);
        result.Result.Any(x => x.Id == created.Id);
    }
}
