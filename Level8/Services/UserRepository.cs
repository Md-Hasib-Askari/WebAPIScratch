using WebAPIScratch.Controllers;
using WebAPIScratch.Domain;
using WebAPIScratch.Interfaces;

namespace WebAPIScratch.Services;

public class UserRepository : IUserRepository
{
    private readonly List<AppUser> _db =
    [
        new AppUser
        {
            Id = 1,
            Name = "Hasib",
            Age = 25,
        },
        new AppUser
        {
            Id = 2,
            Name = "Shawon",
            Age = 24,
        },
        new AppUser
        {
            Id = 3,
            Name = "Jahid",
            Age = 24,
        },
    ];

    public AppUser Add(CreateUserDto dto)
    {
        var newUser = new AppUser
        {
            Id = dto.Id,
            Name = dto.Name,
            Age = dto.Age,
        };
        _db.Add(newUser);
        return newUser;
    }

    public int Count() => _db.Count;

    public IEnumerable<AppUser> GetAll() => _db;
}
