using Bogus;
using RecipeBook.Comunication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build()
        {
            return new Faker<RequestRegisterUserJson>()
                 .RuleFor(user => user.Name, f => f.Person.FullName)
                 .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name.ToLower()))
                 .RuleFor(user => user.Password, f => f.Internet.Password());
        }
    }
}
