using AutoMapper;
using Reprizo.Areas.Admin.ViewModels.BestWorker;
using Reprizo.Areas.Admin.ViewModels.Blog;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Contact;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Areas.Admin.ViewModels.Subscribe;
using Reprizo.Areas.Admin.ViewModels.Team;
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
            CreateMap<Repair, RepairVM>();
            CreateMap<Team, TeamVM>();
            CreateMap<BestWorker, BestWorkerVM>();
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<CategoryVM, CategoryEditVM>();
            CreateMap<CategoryEditVM, Category>();
            CreateMap<SubscribeCreateVM, Subscribe>();
            CreateMap<ContactMessageCreateVM, ContactMessage>();


            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
			CreateMap<Product, ProductDetailVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
							  .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
			CreateMap<ProductCreateVM, Product>();
			CreateMap<ProductEditVM, Product>();
			CreateMap<SliderCreateVM, Slider>();
			CreateMap<SliderEditVM, Slider>();
			CreateMap<SliderVM, SliderEditVM>();
			CreateMap<EssenceVM, EssenceEditVM>();
			CreateMap<EssenceEditVM, Essence>();
			CreateMap<FeatureVM, FeatureEditVM>();
			CreateMap<FeatureEditVM, Feature>();
		}
    }
}
