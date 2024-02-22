using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using ShopMVC8.Data;
using System.Drawing;
namespace ShopMVC8.Controllers
{   
    public class AdminCustomerController : Controller
    {
    private Hshop2023Context db = new Hshop2023Context();
    
    public AdminCustomerController(Hshop2023Context context)
    {
            db = context;
    }

    public IActionResult Index()
    {
            // Truy vấn tất cả khách hàng từ cơ sở dữ liệu
            var allKhachHang = db.KhachHangs.ToList();

            // Truyền danh sách khách hàng đến view để hiển thị
            return View(allKhachHang);
    }
    }
}