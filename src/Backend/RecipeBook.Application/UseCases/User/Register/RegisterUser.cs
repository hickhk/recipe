using AutoMapper;
using RecipeBook.Application.Services.Criptografia;
using RecipeBook.Application.UseCases.User.Interfaces;
using RecipeBook.Comunication.Requests;
using RecipeBook.Comunication.Responses;
using RecipeBook.Domain.Interfaces;
using RecipeBook.Domain.Interfaces.User;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.ExceptionCustom;


namespace RecipeBook.Application.UseCases.User.Register
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IUserReadOnlyInterface _userReadOnlyInterface;
        private readonly IUserWriteOnlyInterface _userWriteOnlyInterface;
        private readonly IMapper _mapper;
        private readonly Encrypt _encrypt;
        private readonly IDataBasePersist _dataBasePercists;


        public RegisterUser(
            IUserReadOnlyInterface userReadOnlyInterface,
            IUserWriteOnlyInterface userWriteOnlyInterface,
            IMapper mapper,
            Encrypt encrypt,
            IDataBasePersist dataBasePercists
            )
        {
            _userReadOnlyInterface = userReadOnlyInterface;
            _userWriteOnlyInterface = userWriteOnlyInterface;
            _mapper = mapper;
            _encrypt = encrypt;
            _dataBasePercists = dataBasePercists;
        }


        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {

            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _encrypt.EncryptPassword(request.Password);

            await _userWriteOnlyInterface.AddUserAsync(user);

            await _dataBasePercists.SaveChanges();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            var userExixts = await _userReadOnlyInterface.ExistUserAsync(request.Email);

            if (userExixts)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResoourcesMessagesException.EMAIL_EXISTIS));
            }

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
