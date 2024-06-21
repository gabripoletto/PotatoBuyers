using CommomTestUtilities.Requests;
using FluentAssertions;
using PotatoBuyers.Application.UseCases.User.Register;
using PotatoBuyers.Exceptions.ResponsesMessages;
using Xunit;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RegisterUserValidatorBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            var request = RegisterUserValidatorBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ErrorMessages.REQUIRED_FIELD));
        }
    }
}
