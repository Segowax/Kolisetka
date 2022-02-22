using AutoMapper;
using Kolisetka.Application.DTOs;
using Kolisetka.Domain;

namespace Kolisetka.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
