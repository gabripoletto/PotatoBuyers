﻿using Bogus;
using CommomTestUtilities.UtilitiesTest;
using PotatoBuyers.Communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RequestLoginJsonBuilder
    {
        public static RequestLoginJson Build(int passwordLength = 10)
        {
            var teste = new Faker<RequestLoginJson>()
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Email))
                .RuleFor(user => user.Password, (f) => PasswordGenerator.GeneratePassword(passwordLength));

            return teste;
        }
    }
}
