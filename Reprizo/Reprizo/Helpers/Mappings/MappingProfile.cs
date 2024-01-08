using AutoMapper;
using Reprizo.Areas.Admin.ViewModels.Blog;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Models;

namespace Reprizo.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Slider, SliderVM>();
            CreateMap<Collection, CollectionVM>();
            CreateMap<Essence, EssenceVM>();
            CreateMap<Feature, FeatureVM>();
            CreateMap<Blog, BlogVM>();

            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
        }
    }
}
