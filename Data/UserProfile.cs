using AutoMapper;
using UserApi.Data.Entities;
using UserApi.Models;

namespace UserApi.Data
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserModel>();
               
            this.CreateMap<User, UserModel>().ReverseMap()
                .ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}
