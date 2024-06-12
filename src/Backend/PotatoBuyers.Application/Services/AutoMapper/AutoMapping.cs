using AutoMapper;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Domain.Entities.Users;

namespace PotatoBuyers.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, UserBase>().ForMember(dest => dest.Password, opt => opt.Ignore());
        }

        private void DomainToResponse()
        {

        }
    }
}
