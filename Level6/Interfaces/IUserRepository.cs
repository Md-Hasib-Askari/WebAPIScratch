using WebAPIScratch.Domain;

namespace WebAPIScratch.Interfaces;

public interface IUserRepository
{
    IEnumerable<AppUser> GetAll();
}
