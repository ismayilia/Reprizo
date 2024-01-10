﻿using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Helpers;

namespace Reprizo.Areas.Admin.ViewModels.Shop
{
    public class ShopVM
    {
        public List<CategoryVM> Categories { get; set; }
        public Paginate<ProductVM> Paginate { get; set; }
    }
}
