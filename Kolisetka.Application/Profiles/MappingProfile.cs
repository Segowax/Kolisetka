using AutoMapper;
using Kolisetka.Application.DTOs;
using Kolisetka.Domain;

namespace Kolisetka.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product Mapping
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductDeleteDto>().ReverseMap();
            #endregion
        }
    }
}
