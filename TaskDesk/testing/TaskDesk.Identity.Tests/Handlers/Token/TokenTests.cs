using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Shouldly;
using TaskDesk.Core.Functional.Tests.Fixtures;
using TaskDesk.Shared.Exceptions;

namespace TaskDesk.Identity.Tests.Handlers.Token;

[Collection(nameof(SliceFixture))]
public class TokenTests
{
    private readonly SliceFixture _fixture;

    public TokenTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Can_Generate_Token_By_Password()
    {
        var faker = new Account.Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeTokenRequest(created, createRequest.Password);
        var result = await _fixture.SendAsync(request);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Can_Generate_Token_By_Google()
    {
        var faker = new Google.Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeTokenRequest(created);
        var result = await _fixture.SendAsync(request);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Throw_Validation_When_UserIdOrEmail_Empty(string userIdOrEmail)
    {
        var faker = new Google.Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeTokenRequest(created);
        request.UserIdOrEmail = userIdOrEmail;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Throw_Validation_When_Password_Empty(string password)
    {
        var faker = new Account.Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeTokenRequest(created, createRequest.Password);
        request.Password = password;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Fact]
    public async Task Throw_NotFound_When_User_Not_Exist_By_Password()
    {
        var faker = new Account.Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeTokenRequest(created, createRequest.Password);
        request.UserIdOrEmail = "SomeNotExistUser";

        await Should.ThrowAsync<NotFoundException>(() => _fixture.SendAsync(request));
    }

    [Fact]
    public async Task Throw_NotFound_When_User_Not_Exist_By_Google()
    {
        var faker = new Google.Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeTokenRequest(created);
        request.UserIdOrEmail = "SomeNotExistUser";

        await Should.ThrowAsync<NotFoundException>(() => _fixture.SendAsync(request));
    }

    [Fact]
    public async Task Throw_BadRequest_When_Wrong_Passwor()
    {
        var faker = new Account.Faker();

        var createRequest = faker.FakeCreateRequest();
        var created = await _fixture.SendAsync(createRequest);

        var request = faker.FakeTokenRequest(created, createRequest.Password);
        request.Password += "123";

        await Should.ThrowAsync<BadRequestException>(() => _fixture.SendAsync(request));
    }
}
