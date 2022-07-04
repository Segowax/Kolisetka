using AutoMapper;
using Kolisetka.MVC.Models.Product;
using Kolisetka.MVC.Services.Base;

namespace Kolisetka.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateDto, ProductCreateVM>().ReverseMap();
            CreateMap<ProductDeleteDto, ProductDeleteVM>().ReverseMap();
            CreateMap<ProductGetDto, ProductGetVM>().ReverseMap();
            CreateMap<ProductUpdateDto, ProductUpdateVM>().ReverseMap();
        }
    }
}
