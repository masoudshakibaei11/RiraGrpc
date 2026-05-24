using FluentValidation;
using Rira.Application.Dtos;
using Rira.Application.Interfaces;

namespace Rira.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Create(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var isExist = await _userRepository.Any(dto.NationalCode, cancellationToken);

        if (isExist)
            throw new ValidationException("کد ملی وجود دارد");

        await _userRepository.Create(dto, cancellationToken);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
       await _userRepository.Delete(id, cancellationToken);
    }

    public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _userRepository.GetAll(cancellationToken);
    }

    public async Task<UserDto?> Get(int id, CancellationToken cancellationToken)
    {
        return await _userRepository.Get(id, cancellationToken);
    }

    public async Task Update(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        await _userRepository.Update(dto, cancellationToken);
    }
}
