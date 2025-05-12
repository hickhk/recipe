namespace RecipeBook.Domain.Interfaces.User
{
    public interface IUserWriteOnlyInterface
    {
        Task AddUserAsync(Entities.User user);
    }
}
