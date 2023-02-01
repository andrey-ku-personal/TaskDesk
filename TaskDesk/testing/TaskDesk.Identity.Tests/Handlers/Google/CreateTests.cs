using TaskDesk.Identity.Tests.Fixtures;
using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Tests.Handlers.Google;

[Collection(nameof(SliceFixture))]

public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_UserModel()
    {
        var request = new Faker().FakeCreateRequest();
        var result = await _fixture.SendAsync(request);

        result.AsSource()
            .OfLikeness<UserModel>()
            .Without(x => x.Id)
            .Without(x => x.Password)
            .Without(x => x.UserId)
            .ShouldEqual(request);
    }
}
