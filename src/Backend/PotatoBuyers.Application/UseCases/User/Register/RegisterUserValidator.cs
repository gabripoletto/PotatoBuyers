using FluentValidation;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Exceptions.ResponsesMessages;
using System.Text.RegularExpressions;

namespace PotatoBuyers.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Cpf).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Telephone).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Password).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            When(user => !string.IsNullOrEmpty(user.Email), () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ErrorMessages.EMAIL_INVALID);
            });
            When(user => !string.IsNullOrEmpty(user.Cpf), () =>
            {
                RuleFor(user => user.Cpf)   
                .Cascade(CascadeMode.Stop)
                .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
                .WithMessage(ErrorMessages.CPF_INVALID_FORMAT)
                .Must(IsCpf)
                .WithMessage(ErrorMessages.CPF_INVALID);
            });
            When(user => !string.IsNullOrEmpty(user.Telephone), () =>
            {
                RuleFor(user => user.Telephone).Matches(@"^\(\d{2}\) \d{5}-\d{4}$").WithMessage(ErrorMessages.TEL_INVALID_FORMAT);
            });
            When(user => !string.IsNullOrEmpty(user.Password), () =>
            {
                RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(8).WithMessage(ErrorMessages.PASSWORD_INVALID_DIGITS);
                When(user => user.Password.Length > 8, () =>
                {
                    RuleFor(user => user.Password).Must(password => HasRequiredCharacters(password)).WithMessage(ErrorMessages.PASSWORD_REQUIRED_DIGITS);
                });
            });
        }

        private bool HasRequiredCharacters(string password)
        {
            return Regex.IsMatch(password, @"\d") ||
                   Regex.IsMatch(password, @"[A-Z]") ||
                   Regex.IsMatch(password, @"[\W_]");
        }

        private static bool IsCpf(string cpf)
        {
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            string digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return cpf.EndsWith(digit);
        }
    }
}
