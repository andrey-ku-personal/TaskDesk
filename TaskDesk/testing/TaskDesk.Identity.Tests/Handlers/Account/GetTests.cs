using Shouldly;
using TaskDesk.Identity.Handlers.Account;
using TaskDesk.Identity.Tests.Fixtures;
using TaskDesk.Shared.Exceptions;

namespace TaskDesk.Identity.Tests.Handlers.Account;

[Collection(nameof(SliceFixture))]
public class GetTests
{
    private readonly SliceFixture _fixture;

    public GetTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Get_User_By_Id()
    {
        var createRequest = new Faker().FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var result = await _fixture.SendAsync(new GetRequest() { Id = created.Id });

        result!.ShouldNotBeNull();
        result!.Id.ShouldBe(created.Id);
    }

    [Fact]
    public async Task Get_User_By_UserId()
    {
        var createRequest = new Faker().FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var result = await _fixture.SendAsync(new GetRequest() { UserIdOrEmail = created.UserId });

        result!.ShouldNotBeNull();
        result!.UserId.ShouldBe(created.UserId);
    }

    [Fact]
    public async Task Get_User_By_Email()
    {
        var createRequest = new Faker().FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var result = await _fixture.SendAsync(new GetRequest() { UserIdOrEmail = created.Email });

        result!.ShouldNotBeNull();
        result!.Email.ShouldBe(created.Email);
    }

    [Fact]
    public async Task Throw_Validation_When_Id_Null_And_UserIdOrEmail_Null()
    {
        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new GetRequest()));
    }
}
