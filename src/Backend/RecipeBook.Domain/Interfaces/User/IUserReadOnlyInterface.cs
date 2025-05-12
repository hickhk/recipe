namespace RecipeBook.Domain.Interfaces.User
{
    public interface IUserReadOnlyInterface
    {
        Task<bool> ExistUserAsync(string email);
    }
}
