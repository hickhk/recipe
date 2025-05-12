using RecipeBook.Domain.Interfaces;

namespace RecipeBook.Infrastructure.DataAccess
{
    public class DataBasePersist : IDataBasePersist
    {
        private readonly RecipeBookDbContext _dbContext;

        public DataBasePersist(RecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task SaveChanges() => _dbContext.SaveChanges();
    }
}
