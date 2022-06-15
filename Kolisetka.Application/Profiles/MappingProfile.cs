using AutoMapper;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Domain;

namespace Kolisetka.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product Mapping
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductDeleteDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            #endregion
        }
    }
}
