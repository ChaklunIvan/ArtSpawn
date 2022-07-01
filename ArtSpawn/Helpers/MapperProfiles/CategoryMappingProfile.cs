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
            CreateMap<CategoryRequest, Category>()
                .ForMember(category => category.Type, opt =>
                opt.MapFrom(category => category.Type));

            CreateMap<Category, CategoryResponse>();

            CreateMap<CategoryUpdate, Category>();
        }
    }
}
