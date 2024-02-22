using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ShopMVC8.Data;
using ShopMVC8.Helper;
using ShopMVC8.ViewModels;
using System.Security.Claims;


namespace ShopMVC8.Controllers
{

    public class KhachHangController : Controller
    {
        private readonly Hshop2023Context db;
        private readonly IMapper _mapper;

        // Khach hang Controller

        public KhachHangController(Hshop2023Context context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
        [HttpGet]
        
        [Route("admin/khachhang/index")]
        public IActionResult Index()
        {
            // Truy vấn tất cả khách hàng từ cơ sở dữ liệu
            var allKhachHang = db.KhachHangs.ToList();

            // Truyền danh sách khách hàng đến view để hiển thị
            return View(allKhachHang);
        }

        [HttpGet]
        // Dang Ky
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = Util.GenerateRandomkey();
                    khachHang.MatKhau = model.MatKhau?.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;
                    khachHang.VaiTro = 0;

                    // Kiểm tra và xử lý hình ảnh
                    if (Hinh != null)
                    {
                        khachHang.Hinh = Util.UpLoadHinh(Hinh, "KhachHang");
                    }

                    // Thêm người dùng mới vào dataset
                    db.Add(khachHang);
                    db.SaveChanges();
                    // Redirect đến trang đăng nhập
                    return RedirectToAction("DangNhap", "KhachHang");
                }
                catch (Exception) {  }
            }
            return View();
        }
   
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.Username);
                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Không có khách hàng này");

                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "Tài khoản bị khóa");
                    }
                    else
                    {
                        if (khachHang.MatKhau!= model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("loi", "Sai mật khẩu");
                        }
                        else
                        {
                            var claims = new List<Claim> {
                            new(ClaimTypes.Email,khachHang.Email),
                            new(ClaimTypes.Name,khachHang.HoTen),
                            new(MySetting.CLAIM_CUSTOMERID,khachHang.MaKh),
                            new(ClaimTypes.Role,"Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else { return Redirect("/"); }
                        }   
                    }
                }
            }
            return View();
        }
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
        // Kiểm tra xem người dùng đã đăng nhập hay chưa
        if (User?.Identity?.IsAuthenticated == true)
        {
         // Thực hiện đăng xuất
         await HttpContext.SignOutAsync();
         // Chuyển hướng về trang chủ
         return Redirect("/");
        }

     // Nếu người dùng chưa đăng nhập, bạn có thể thực hiện xử lý khác ở đây
     // Ví dụ: return RedirectToAction("Login", "Account");
        return RedirectToAction("Index", "Home");
        }
    }
}
        
