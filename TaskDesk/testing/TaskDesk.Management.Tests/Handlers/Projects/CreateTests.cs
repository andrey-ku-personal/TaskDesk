using Shouldly;
using TaskDesk.Management.Tests.Fixtures;
using TaskDesk.Shared.Exceptions;
using TaskDesk.SharedModel.Handlers.Project.Models;

namespace TaskDesk.Management.Tests.Handlers.Projects;

[Collection(nameof(SliceFixture))]
public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Can_Create_Project()
    {
        var request = new Faker().FakeCreateRequest();
        var result = await _fixture.SendAsync(request);

        result.AsSource()
            .OfLikeness<ProjectModel>()
            .Without(x => x.Id)
            .ShouldEqual(request);
    }

    [Fact]
    public async Task Validation_exception_When_Id_None_Zero()
    {
        var request = new Faker().FakeCreateRequest();
        request.Id = int.MaxValue;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Validation_exception_On_Empty_Name(string name)
    {
        var request = new Faker().FakeCreateRequest();
        request.Name = name;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }
}
