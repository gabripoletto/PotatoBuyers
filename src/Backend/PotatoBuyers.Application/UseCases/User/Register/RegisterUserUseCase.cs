using PotatoBuyers.Application.Services.AutoMapper;
using PotatoBuyers.Application.Services.Cryptography;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;
using PotatoBuyers.Domain.Entities.Users;
using PotatoBuyers.Domain.Repositories.User;
using PotatoBuyers.Exceptions.ExceptionsBase;

namespace PotatoBuyers.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;

        public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = userReadOnlyRepository;   
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            Validate(request);

            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            var user = autoMapper.Map<UserBase>(request);

            var cryptography = new PasswordEncripter();

            user.Password = cryptography.Encrypt(request.Password);

            await _writeOnlyRepository.Insert(user);

            return null;
        } 

        private void Validate(RequestRegisterUserJson request)
        {
            RegisterUserValidator validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
