using AutoMapper;
using Kolisetka.MVC.Models.Product;
using Kolisetka.MVC.Models.User;
using Kolisetka.MVC.Services.Base;

namespace Kolisetka.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product
            CreateMap<ProductCreateDto, ProductCreateVM>().ReverseMap();
            CreateMap<ProductDeleteDto, ProductDeleteVM>().ReverseMap();
            CreateMap<ProductGetDto, ProductGetVM>().ReverseMap();
            CreateMap<ProductUpdateDto, ProductUpdateVM>().ReverseMap();
            #endregion

            #region User
            CreateMap<GetUserRequest, LoginVM>().ReverseMap();
            CreateMap<UserCreateDto, RegisterVM>().ReverseMap();
            CreateMap<UserGetDto, UserGetVM>().ReverseMap();
            #endregion
        }
    }
}
