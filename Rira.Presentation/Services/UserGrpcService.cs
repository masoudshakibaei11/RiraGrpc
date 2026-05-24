using FluentValidation;
using Grpc.Core;
using Rira.Application.Dtos;
using Rira.Application.Interfaces;
using Rira.Grpc;
using Empty = Google.Protobuf.WellKnownTypes.Empty;

namespace Rira.Presentation.Services;

public class UserGrpcService : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public UserGrpcService(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<Result> Create(
        CreateUserRequest request,
        ServerCallContext context)
    {
        var createUserDto = new CreateUserDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
            BirthDate = request.BirthDate.ToDateTime() 
        };

        await _userService.Create(createUserDto, context.CancellationToken);

        return new Result { IsSuccess = true };

    }

    public override async Task<UserResponse> Get(
    GetUserRequest request,
    ServerCallContext context)
    {
        if (request.Id <= 0)
        {
            throw new ValidationException("شناسه کاربری باید عددی بزرگتر از صفر باشد.");
        }

        var user = await _userService.Get(request.Id, context.CancellationToken);

        return new UserResponse
        {
            Id = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NationalCode = user.NationalCode,
            BirthDate = user.BirthDate.ToString()
        };
    }


    public override async Task<Result> Update(
    UpdateUserRequest request,
    ServerCallContext context)
    {
        var updateUserDto = new UpdateUserDto
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
            BirthDate = request.BirthDate.ToDateTime()
        };
        await _userService.Update(updateUserDto, context.CancellationToken);

        return new Result { IsSuccess = true };
    }

    public override async Task<Result> Delete(
    DeleteUserRequest request,
    ServerCallContext context)
    {
        if (request.Id <= 0)
        {
            throw new ValidationException("شناسه کاربری باید عددی بزرگتر از صفر باشد.");
        }
        await _userService.Delete(request.Id, context.CancellationToken);

        return new Result { IsSuccess = true };
    }

    public override async Task<UserListResponse> GetAll(
    Empty request,
    ServerCallContext context)
    {
        var users = await _userService.GetAll(context.CancellationToken);

        var response = new UserListResponse();

        response.Users.AddRange(users.Select(u => new UserResponse
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            NationalCode = u.NationalCode,
            BirthDate = u.BirthDate.ToString()
        }));

        return response;
    }

}
