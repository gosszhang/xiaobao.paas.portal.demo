using AutoMapper;
using Xiaobao.PaaS.Portal.Server.Models;

namespace Xiaobao.PaaS.Portal.Server.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddUserRequest, UserModel>();
            CreateMap<EditUserRequest, UserModel>();
        }
    }
}
