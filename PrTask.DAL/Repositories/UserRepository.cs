using System;
using System.Threading.Tasks;
using PrTask.DAL.Domain;
using PrTask.DAL.Repositories.Abstract;

namespace PrTask.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PrTaskSqlContext _context;

        public UserRepository(PrTaskSqlContext context)
        {
            _context = context;
        }

        public async Task<Guid> UpdateUsers(UserEnt user)
        {
            await _context.UserEnt.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}