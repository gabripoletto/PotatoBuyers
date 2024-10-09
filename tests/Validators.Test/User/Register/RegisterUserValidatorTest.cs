using CommomTestUtilities.Requests;
using FluentAssertions;
using PotatoBuyers.Application.UseCases.User.Register;
using PotatoBuyers.Exceptions.ResponsesMessages;
using System.Text.RegularExpressions;
using Xunit;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.REQUIRED_FIELD));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.REQUIRED_FIELD));
        }

        [Fact]
        public void Error_Email_Invalid_Address()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "ErrorTeste@@";
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.EMAIL_INVALID));
        }

        [Fact]
        public void Error_Cpf_Empty()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Cpf = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.REQUIRED_FIELD));
        }

        [Fact]
        public void Error_Cpf_Invalid()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Cpf = "231.123.123-22";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.CPF_INVALID));
        }

        [Fact]
        public void Error_Cpf_Invalid_Format()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Cpf = "23112312322";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.CPF_INVALID_FORMAT));
        }

        [Fact]
        public void Error_Telephone_Empty()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Telephone = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.REQUIRED_FIELD));
        }

        [Fact]
        public void Error_Telefone_Invalid_Format()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Telephone = "19999999999";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.TEL_INVALID_FORMAT));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void Error_Password_Invalid_Length(int passwordLength)
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.PASSWORD_INVALID_DIGITS));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Error_Password_Invalid_Digits(int passwordType)
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = InvalidPasswordType(passwordType);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.PASSWORD_INVALID_DIGITS));
        }

        private static string InvalidPasswordType(int type)
        {
            string password = string.Empty;

            switch (type)
            {
                case 1:
                    password = GeneratePasswordWithNumber();
                    break;
                case 2:
                    password = GeneratePasswordWithUppercase();
                    break;
                case 3:
                    password = GeneratePasswordWithSpecialCharacter();
                    break;
                default:
                    password = "123456789";
                    break;
            }

            return password;
        }

        private static string GeneratePasswordWithNumber()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            return GenerateInvalidPassword(chars, numbers, @"\d");
        }

        private static string GeneratePasswordWithUppercase()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return GenerateInvalidPassword(chars, uppercase, @"[A-Z]");
        }

        private static string GeneratePasswordWithSpecialCharacter()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string specialChars = "!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?";
            return GenerateInvalidPassword(chars, specialChars, @"[\W_]");
        }

        private static string GenerateInvalidPassword(string chars, string extraChars, string regexPattern)
        {
            Random random = new Random();

            string password;
            do
            {
                char[] buffer = new char[9];
                for (int i = 0; i < 8; i++)
                {
                    buffer[i] = chars[random.Next(chars.Length)];
                }
                buffer[8] = extraChars[random.Next(extraChars.Length)];
                password = new string(buffer);
            } while (!Regex.IsMatch(password, regexPattern));

            return password;
        }
    }
}
