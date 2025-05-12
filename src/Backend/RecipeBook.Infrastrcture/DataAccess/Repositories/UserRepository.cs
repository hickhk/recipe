using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Interfaces.User;

namespace RecipeBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyInterface, IUserWriteOnlyInterface
    {
        private readonly RecipeBookDbContext _dbContext;

        public UserRepository(RecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task AddUserAsync(User user) => await _dbContext.Users.AddAsync(user);

        public async Task<bool> ExistUserAsync(string email) => await _dbContext.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);

    }
}
