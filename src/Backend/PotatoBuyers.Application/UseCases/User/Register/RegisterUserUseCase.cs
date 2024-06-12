using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;
using PotatoBuyers.Exceptions.ExceptionsBase;

namespace PotatoBuyers.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
        {
            Validate(request);


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
