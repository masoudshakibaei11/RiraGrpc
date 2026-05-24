using Rira.Application.Dtos;

namespace Rira.Application.Interfaces;

public interface IUserRepository
{
    Task Create(CreateUserDto dto, CancellationToken cancellationToken);
    Task<UserDto?> Get(int id, CancellationToken cancellationToken);
    Task<List<UserDto>> GetAll(CancellationToken cancellationToken);
    Task Update(UpdateUserDto dto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<bool> Any(string nationalCode, CancellationToken cancellationToken);
}
