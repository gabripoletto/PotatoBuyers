using PotatoBuyers.Exceptions.ResponsesMessages;

namespace PotatoBuyers.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : PotatoBuyersExceptions
    {
        public InvalidLoginException() : base(ErrorMessages.EMAIL_OR_PASSWORD_INVALID) { }
    }
}
