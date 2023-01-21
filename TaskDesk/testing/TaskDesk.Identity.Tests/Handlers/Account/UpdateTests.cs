using Shouldly;
using TaskDesk.Identity.Tests.Fixtures;
using TaskDesk.Shared.Exceptions;
using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Tests.Handlers.Account;

[Collection(nameof(SliceFixture))]
public class UpdateTests
{
    private readonly SliceFixture _fixture;

    public UpdateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Can_Update()
    {
        var faker = new Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var updateRequest = faker.FakeUpdateRequest(created);
        var result = await _fixture.SendAsync(updateRequest);

        result.AsSource()
            .OfLikeness<UserModel>()
            .Without(x => x.Id)
            .Without(x => x.Password)
            .Without(x => x.UserId)
            .ShouldEqual(result);
    }

    [Fact]
    public async Task Can_Update_Second_Time()
    {
        var faker = new Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var updateRequest = faker.FakeUpdateRequest(created);
        var result1 = await _fixture.SendAsync(updateRequest);

        updateRequest = faker.FakeUpdateRequest(result1);
        var result2 = await _fixture.SendAsync(updateRequest);

        result2.AsSource()
            .OfLikeness<UserModel>()
            .Without(x => x.Id)
            .Without(x => x.Password)
            .Without(x => x.UserId)
            .ShouldEqual(result2);
    }

    [Fact]
    public async Task Get_ValidationExcecption_On_Zero_Id()
    {
        var faker = new Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeUpdateRequest(created);
        request.Id = 0;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Empty_FirstName(string firstName)
    {
        var faker = new Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeUpdateRequest(created);
        request.FirstName = firstName;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Empty_LastName(string lastName)
    {
        var faker = new Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeUpdateRequest(created);
        request.LastName = lastName;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("12345678901a")]
    public async Task Get_ValidationExcecption_On_Invalid_Email(string email)
    {
        var faker = new Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeUpdateRequest(created);
        request.Email = email;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }
}
