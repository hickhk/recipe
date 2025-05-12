using FluentValidation;
using RecipeBook.Comunication.Requests;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResoourcesMessagesException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResoourcesMessagesException.EMAIL_EMPTY).EmailAddress().WithMessage(ResoourcesMessagesException.EMAIL_INVALID);
            RuleFor(user => user.Password).NotEmpty().WithMessage(ResoourcesMessagesException.PASSWORD_EMPTY);
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResoourcesMessagesException.PASSWORD_TOO_SHORT);
        }
    }
}
