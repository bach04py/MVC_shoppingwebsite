using Microsoft.AspNetCore.Mvc;
using ShopMVC8.Data;
using ShopMVC8.ViewModels;

namespace ShopMVC8.ViewComponents
{
    public class DetailSlideViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;
        public DetailSlideViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var Items = db.HangHoas.AsQueryable(); 
            var result = Items.Select(p => new HangHoaVM 
        { 
            MaHh = p.MaHh,
            TenHh = p.TenHh ,
            DonGia = p.DonGia ?? 0,
            Hinh = p.Hinh ?? "",
            TenLoai = p.MaLoaiNavigation.TenLoai
         });
            var randomItems = result.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            return View("DetailSlide",randomItems);
        }
   }
}