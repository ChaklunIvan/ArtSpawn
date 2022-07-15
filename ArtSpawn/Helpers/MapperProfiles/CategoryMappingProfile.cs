using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using AutoMapper;

namespace ArtSpawn.Helpers.MapperProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryRequest, Category>();

            CreateMap<Category, CategoryResponse>()
                .ForMember(c => c.Category,
                opt => opt.MapFrom(m => m.Type));

            CreateMap<CategoryUpdate, Category>();
        }
    }
}
