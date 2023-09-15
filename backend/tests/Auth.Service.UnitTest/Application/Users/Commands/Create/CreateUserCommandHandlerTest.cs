using Auth.Service.Application.Models.ViewModels;
using Auth.Service.Application.Users.Commands.Create;
using Auth.Service.Mail.Interfaces;
using Auth.Service.Persistence;
using Auth.Service.UnitTest._Builders.Commands.Users;
using Auth.Service.UnitTest._Extensions;
using Microsoft.EntityFrameworkCore;
using NSubstitute.ExceptionExtensions;

namespace Auth.Service.UnitTest.Application.Users.Commands.Create;

public class CreateUserCommandHandlerTest
{
    readonly IAuthContext _authContext;
    readonly IPasswordHashService _passwordHashService;
    readonly IFluentEmailService _mailService;
    readonly IUserVerificationTokenService _userTokenService;

    readonly CreateUserCommandHandler _sut;

    CreateUserCommandBuilder _command;

    public CreateUserCommandHandlerTest()
    {
        _authContext = Substitute.For<IAuthContext>();
        _passwordHashService = Substitute.For<IPasswordHashService>();
        _mailService = Substitute.For<IFluentEmailService>();
        _userTokenService = Substitute.For<IUserVerificationTokenService>();

        _command = CreateUserCommandBuilder.New();

        var users = new List<User>()
        {
        }.AsDbSet();

        _authContext.Users.Returns(users);

        _sut = new CreateUserCommandHandler(_authContext, _passwordHashService, _mailService, _userTokenService);
    }

    [Fact]
    public async Task Must_ThrowArgumentNullException_WhenRequestIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.Handle(null!, CancellationToken.None));
    }

    [Fact]
    public async Task Must_ThrowUserAlreadyExistsException_When_User_Is_Active()
    {
        var request = _command.Build();

        var users = new List<User>()
        {
            new User()
            {
                Email = request.Email,
                Active = true
            }
        }.AsDbSet();

        _authContext.Users.Returns(users);

        await Assert.ThrowsAsync<UserAlreadyExistsException>(() => _sut.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Must_AddUserToContext_WhenRequestIsValid()
    {
        var request = _command.Build();

        await _sut.Handle(request, CancellationToken.None);

        _authContext.Users.Received(1).Add(Arg.Is<User>(e => e.Name.Equals(request.Name)));
    }

    [Fact]
    public async Task Must_SaveChangesAsync_WhenRequestIsValid()
    {
        var request = _command.Build();

        await _sut.Handle(request, CancellationToken.None);

        await _authContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Must_ReturnCreateSuccess_WhenRequestIsValid()
    {
        var request = _command.Build();

        var result = await _sut.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<CreateSuccess>(result);
    }

    [Fact]
    public async Task Must_CallHashPassword_WhenRequestIsValid()
    {
        var request = _command.Build();
        _passwordHashService.HashPassword(request.Password).Returns("hashed_password");

        await _sut.Handle(request, CancellationToken.None);

        _passwordHashService.Received(1).HashPassword(request.Password);
    }

    [Fact]
    public async Task Must_ThrowPersistenceException_WhenDbUpdateExceptionOccurs()
    {
        _authContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Throws(new DbUpdateException());

        var request = _command.Build();

        await Assert.ThrowsAsync<PersistenceException>(() => _sut.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Must_Call_SendConfirmationEmailAsync_When_User_Is_Not_Null()
    {
        var request = _command.Build();

        await _sut.Handle(request, CancellationToken.None);

        await _mailService.Received(1).SendConfirmationEmailAsync(Arg.Any<string>(),Arg.Any<string>(), Arg.Any<string>());
    }
}