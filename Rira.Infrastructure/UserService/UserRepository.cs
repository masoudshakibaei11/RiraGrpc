using Microsoft.EntityFrameworkCore;
using Rira.Application.Dtos;
using Rira.Application.Interfaces;
using Rira.Domain.Entities;
using Rira.Infrastructure.Persistence;

namespace Rira.Infrastructure.UserService;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(
        CreateUserDto request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
            BirthDate = request.BirthDate,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);


    }


    public async Task<UserDto?> Get(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException("کاربر یافت نشد");

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NationalCode = user.NationalCode,
            BirthDate = user.BirthDate,
        };
    }

    public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Users.Select(
            s => new UserDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                NationalCode = s.NationalCode,
                BirthDate = s.BirthDate,
            }).ToListAsync(cancellationToken);
    }

    public async Task Update(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(dto.Id, cancellationToken);

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.NationalCode = dto.NationalCode;
        user.BirthDate = dto.BirthDate;

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException("کاربر یافت نشد");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> Any(string nationalCode, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(u => u.NationalCode == nationalCode, cancellationToken);
    }
}
