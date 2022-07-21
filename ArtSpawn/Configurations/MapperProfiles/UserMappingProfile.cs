using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using AutoMapper;

namespace ArtSpawn.Configurations.MapperProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>()
                .ForMember(u => u.FullName,
                opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));

            CreateMap<UserUpdate, User>();
        }
    }
}
