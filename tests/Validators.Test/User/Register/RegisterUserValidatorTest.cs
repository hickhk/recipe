using CommonTestUtilities.Requests;
using RecipeBook.Application.UseCases.User.Register;
using Shouldly;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.ShouldNotBeNull();
            result.IsValid.ShouldBeTrue();
        }
    }
}
