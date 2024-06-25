using FluentValidation;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Exceptions.ResponsesMessages;

namespace PotatoBuyers.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Cpf).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Telefone).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Password).NotEmpty().WithMessage(ErrorMessages.REQUIRED_FIELD);
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ErrorMessages.PASSWORD_INVALID_DIGITS);
            RuleFor(user => user.Password).Matches(@"\d").WithMessage(ErrorMessages.PASSWORD_REQUIRED_DIGITS);
            RuleFor(user => user.Password).Matches(@"[A-Z]").WithMessage(ErrorMessages.PASSWORD_REQUIRED_DIGITS);
            RuleFor(user => user.Password).Matches(@"[\W_]").WithMessage(ErrorMessages.PASSWORD_REQUIRED_DIGITS);
            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ErrorMessages.EMAIL_INVALID);
            });
            When(user => string.IsNullOrEmpty(user.Cpf) == false, () =>
            {
                RuleFor(user => user.Cpf).Must(IsCpf).WithMessage(ErrorMessages.CPF_INVALID);
            });
            When(user => string.IsNullOrEmpty(user.Telefone) == false, () =>
            {
                RuleFor(user => user.Telefone).Matches(@"^\(\d{2}\) \d{5}-\d{4}$").WithMessage(ErrorMessages.TEL_INVALID);
            });
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
