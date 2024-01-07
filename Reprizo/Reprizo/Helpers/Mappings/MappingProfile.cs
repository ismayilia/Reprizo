using AutoMapper;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Models;

namespace Reprizo.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Slider, SliderVM>();
        }
    }
}
