using System;
using System.Threading.Tasks;
using PrTask.DAL.Domain;

namespace PrTask.DAL.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<Guid> UpdateUsers(UserEnt user);
        Task<UserEnt> SelectUserByLogin(string login);
    }
}