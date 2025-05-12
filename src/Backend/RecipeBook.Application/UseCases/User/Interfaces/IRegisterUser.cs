using RecipeBook.Comunication.Requests;
using RecipeBook.Comunication.Responses;

namespace RecipeBook.Application.UseCases.User.Interfaces
{
    public interface IRegisterUser
    {
        Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
