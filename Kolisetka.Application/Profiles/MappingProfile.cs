using AutoMapper;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Domain;
using Kolisetka.Domain.Models;

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
            #region User Mapping
            CreateMap<User, UserCreateDto>()
                .ForMember(member => member.Password, opt => opt.Ignore())
                .ReverseMap();
            #endregion
        }
    }
}
