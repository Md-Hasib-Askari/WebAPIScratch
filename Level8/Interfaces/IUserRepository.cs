using WebAPIScratch.Controllers;
using WebAPIScratch.Domain;

namespace WebAPIScratch.Interfaces;

public interface IUserRepository
{
    IEnumerable<AppUser> GetAll();
    AppUser Add(CreateUserDto dto);
    int Count();
}
