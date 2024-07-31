using AutoMapper;
using Azure;
using PotatoBuyers.Application.Services.Cryptography;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;
using PotatoBuyers.Domain.Entities.Users;
using PotatoBuyers.Domain.Repositories.User;
using PotatoBuyers.Exceptions.ExceptionsBase;
using PotatoBuyers.Exceptions.ResponsesMessages;
using PotatoBuyers.Infrastructure.DataAccess.Repositories;

namespace PotatoBuyers.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository writeOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            PasswordEncripter passwordEncripter
            )
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = userReadOnlyRepository; 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<UserBase>(request);

            user.Password = _passwordEncripter.Encrypt(request.Password);

            await _writeOnlyRepository.Insert(user);

            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Response = request.Name,
            };
        } 

        private async Task Validate(RequestRegisterUserJson request)
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

            if (emailExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ErrorMessages.EMAIL_INVALID));
            
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
