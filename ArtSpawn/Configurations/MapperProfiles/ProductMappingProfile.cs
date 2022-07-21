using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using AutoMapper;

namespace ArtSpawn.Configurations.MapperProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductRequest, Product>();

            CreateMap<Product, ProductResponse>();

            CreateMap<ProductUpdate, Product>();
        }
    }
}
