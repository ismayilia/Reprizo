using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Slider;

namespace Reprizo.Areas.Admin.ViewModels.Home
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<ProductVM> Products { get; set; }
        public List<EssenceVM> Essences { get; set; }
        public CollectionVM Collection { get; set; }
        public FeatureVM Feature { get; set; }
    }
}
