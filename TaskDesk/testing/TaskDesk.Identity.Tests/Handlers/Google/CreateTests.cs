using TaskDesk.Core.Functional.Tests.Fixtures;
using TaskDesk.Identity.Handlers.Account.Models;

namespace TaskDesk.Identity.Tests.Handlers.Google;

[Collection(nameof(SliceFixture))]

public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_UserModel()
    {
        var command = new Faker().FakeCreateCommand();
        var result = await _fixture.SendAsync(command);

        result.AsSource()
            .OfLikeness<UserModel>()
            .Without(x => x.Id)
            .Without(x => x.Password)
            .Without(x => x.UserId)
            .ShouldEqual(command);
    }
}
