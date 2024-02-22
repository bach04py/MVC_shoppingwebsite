using Microsoft.AspNetCore.Mvc;
using ShopMVC8.Data;
using ShopMVC8.ViewModels;

namespace ShopMVC8.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;
        public MenuLoaiViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select( lo => new MenuLoaiVM 
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            });
            return View("Side_bar_Shop",data); // Default.cshtml 
        }
   }
}