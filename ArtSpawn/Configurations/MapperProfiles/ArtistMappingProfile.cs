using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using AutoMapper;

namespace ArtSpawn.Configurations.MapperProfiles
{
    public class ArtistMappingProfile : Profile
    {
        public ArtistMappingProfile()
        {
            CreateMap<ArtistRequest, Artist>();

            CreateMap<Artist, ArtistResponse>();

            CreateMap<ArtistUpdate, Artist>();

        }
    }
}
