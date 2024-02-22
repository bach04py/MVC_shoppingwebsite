using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMVC8.Data;
using ShopMVC8.ViewModels;
using ShopMVC8.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ShopMVC8.Controllers
{
    public class CartController : Controller
    {
        private readonly Hshop2023Context db;

        public CartController(Hshop2023Context context)
        {
            db = context;
        }
    
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }

        
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy mặt hàng";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.MaHh,
                    TenHh = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity

                };
                giohang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, giohang);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(Cart);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
              var customerId =  HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)!.Value;
              var hoadon = new HoaDon
             {
                MaKh = customerId,
                HoTen = model.HoTen,
                DiaChi = model.DiaChi!,
                GhiChu = model.GhiChu,
                NgayDat = DateOnly.FromDateTime(DateTime.Now),
                CachThanhToan = "COD",
                CachVanChuyen = "GRAB",
                MaTrangThai = 0,
              };

                db.Add(hoadon);
                db.SaveChanges();   
                var cthd = new List<ChiTietHd>();
                foreach (var item in Cart)
                {
                        cthd.Add(new ChiTietHd
                        {
                            MaHd = hoadon.MaHd,
                            SoLuong = item.SoLuong,
                            DonGia = item.DonGia,
                            MaHh = item.MaHh,
                            GiamGia = 0
                        }
                    );
                }   
                db.AddRange(cthd);
                db.SaveChanges();
                var cartItems = new List<CartItem>();
                HttpContext.Session.Set(MySetting.CART_KEY, cartItems);
                return View("Success");
            }
            return View(Cart);
        }
    }
}