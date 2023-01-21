using Shouldly;
using TaskDesk.Identity.Tests.Fixtures;
using TaskDesk.Shared.Exceptions;
using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Tests.Handlers.Account;

[Collection(nameof(SliceFixture))]
public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Can_Create_User()
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

    [Theory]
    [InlineData(1)]
    public async Task Get_ValidationExcecption_On_Not_Zero_Id(int id)
    {
        var request = new Faker().FakeCreateRequest();
        request.Id = id;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Empty_FirstName(string firstName)
    {
        var request = new Faker().FakeCreateRequest();
        request.FirstName = firstName;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Empty_LastName(string lastName)
    {
        var request = new Faker().FakeCreateRequest();
        request.FirstName = lastName;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData("              ")]
    [InlineData(null)]
    [InlineData("123456789")]
    [InlineData("123456789012")]
    [InlineData("12345678901a")]
    [InlineData("12345678901A")]
    [InlineData("12345678901@")]
    public async Task Get_ValidationExcecption_On_Invalid_Password(string password)
    {
        var request = new Faker().FakeCreateRequest();
        request.Password = password;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("12345678901a")]
    public async Task Get_ValidationExcecption_On_Invalid_Email(string email)
    {
        var request = new Faker().FakeCreateRequest();
        request.Email = email;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(request));
    }
}
