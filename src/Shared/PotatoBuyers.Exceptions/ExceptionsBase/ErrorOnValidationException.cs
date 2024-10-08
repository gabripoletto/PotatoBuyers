﻿namespace PotatoBuyers.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : PotatoBuyersExceptions
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
        {
            ErrorMessages = errorMessages;
        } 
    }
}
