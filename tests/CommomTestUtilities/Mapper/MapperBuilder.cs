﻿using AutoMapper;
using PotatoBuyers.Application.Services.AutoMapper;

namespace CommomTestUtilities.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            return new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();
        }
    }
}
