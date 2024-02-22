using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopMVC8.ViewModels;
using ShopMVC8.Data;
using ShopMVC8.Helper;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using System.Linq.Expressions;
using System.Text;
namespace ShopMVC8.Controllers
{   
       public class AdminCategoryController : Controller
    {
    private Hshop2023Context db = new Hshop2023Context();
    private readonly IMapper _mapper;
   
    public AdminCategoryController(Hshop2023Context context, IMapper mapper)
    {
            db = context;
            _mapper = mapper;
    }
        [Route("Admin/Category")]
        public IActionResult Index()
        {
            var items = db.HangHoas;

            return View(items);
        }

    
        
    [HttpGet]
    [Route("Admin/Category/Adding")]
        // Dang Ky
        public IActionResult Adding()
        {
            return View();
        }

    [HttpPost]
    [Route("Admin/Category/Adding")]
    public IActionResult Adding(QuanliHangHoaVM model, IFormFile Hinh)
        {
                if (ModelState.IsValid)
            {
                    var HangHoaMoi  = _mapper.Map<HangHoa>(model);
                    var category = _mapper.Map<Loai>(model);
                    var existingCategory = db.Loais.FirstOrDefault<Loai>(c => c.MaLoai == category.MaLoai);
                    if (existingCategory == null)
                    {
                        db.Add(category);
                        db.SaveChanges();
                    }
                    HangHoaMoi.SoLanXem = 0 ; 
                    // Kiểm tra và xử lý hình ảnh
                    if (Hinh != null)
                    {
                      HangHoaMoi.Hinh = Util.UpLoadHinh(Hinh, "HangHoa");
                    }
                    // Thêm hàng hoá mới
                    db.Add(HangHoaMoi);
                    db.SaveChanges();
                    // Redirect đến trang quản lí
                    return RedirectToAction("Index", "AdminCategory");
                }

            return View();
        }

        [HttpGet]
        [Route("Admin/Category/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var hangHoa = db.HangHoas.Find(id);

            if (hangHoa == null)
            {
                return View("Notfound","AdminCategory");
            }
            return View(hangHoa);
        }

        [HttpPost]
        public IActionResult EditConfirmed(HangHoa model, IFormFile Hinh)
        {
                var existingCategory = db.Loais.FirstOrDefault<Loai>(c => c.MaLoai == model.MaLoai);
                if (existingCategory != null)
                {
                    db.Update(model);
                    db.SaveChanges();
                }
                model.SoLanXem = 0;
                // Kiểm tra và xử lý hình ảnh
                if (Hinh != null)
                {
                    model.Hinh = Util.UpLoadHinh(Hinh, "HangHoa");
                }
                // Thêm hàng hoá mới
                db.Update(model);
                db.SaveChanges();
                // Redirect đến trang quản lí
                return RedirectToAction("Index", "AdminCategory");
        }
        [HttpGet]
        [Route("Admin/Category/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var hangHoa = db.HangHoas.Find(id);

            if (hangHoa == null)
            {
                return View("Notfound","AdminCategory");
            }
            return View(hangHoa);
        }

    
        public IActionResult DeleteConfirmed(int id)
        { 
             var product = db.HangHoas.Find(id);
            if (product != null)
            {
                // Xóa hàng hoá
                db.Remove(product);
                db.SaveChanges();
                // Redirect đến trang quản lý
                return RedirectToAction("Index", "AdminCategory");
            }
        return View("Notfound","AdminCategory");
        }

        [Route("Admin/Category/NotFound")]
        public IActionResult Notfound()
        {
            return View();
        }

        [HttpGet]
        [Route("Admin/Category/Admore")]
        // Them file excel
        
        public IActionResult Admore()
        {
            return View();
        }
       
       
        public IActionResult AdmoreConfirmed(IFormFile file)
        {
    
            if (file != null && file.Length > 0)
        {
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;               
                for (int row = 2; row <= rowCount; row++)
                {
                    DateTime dateTime = (DateTime)worksheet.Cells[row, 8].Value;
                   
                    var imageValue = worksheet.Cells[row, 7].Value;
                    string fileName="";
                    if (imageValue != null && imageValue is byte[] imageData)
                    {
                        // Tạo đối tượng Image từ dữ liệu byte[]
                         using (var ms = new MemoryStream(imageData))
                    {
                        var image = Image.Load(ms);
                        // Tạo tên file mới cho ảnh
                        fileName = Guid.NewGuid().ToString() + ".jpg";
                        // Lưu ảnh vào file 
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "HangHoa", fileName);
                        string filePath = Path.Combine(uploadsFolder, fileName);
                        image.Save(filePath, new JpegEncoder());
                    }}

                    //ADD Quản lí hàng hoá
                    QuanliHangHoaVM item = new QuanliHangHoaVM{
                    MaHh = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                    TenHh = worksheet.Cells[row, 2].Value?.ToString(),
                    TenAlias = worksheet.Cells[row, 3].Value?.ToString(),
                    MaLoai = Convert.ToInt32(worksheet.Cells[row, 4].Value),
                    MoTaDonVi = worksheet.Cells[row, 5].Value?.ToString(),
                    DonGia = Convert.ToDecimal(worksheet.Cells[row, 6].Value),
                    Hinh = fileName,
                    NgaySx = DateOnly.FromDateTime(dateTime),
                    GiamGia = Convert.ToDecimal(worksheet.Cells[row, 9].Value),
                    MoTa = worksheet.Cells[row, 10].Value?.ToString(),
                    MaNcc = worksheet.Cells[row, 11].Value?.ToString()};
                 
                    var HangHoaMoi  = _mapper.Map<HangHoa>(item);
                    var category = _mapper.Map<Loai>(item);
                    var existingCategory = db.Loais.FirstOrDefault<Loai>(c => c.MaLoai == category.MaLoai);
                    if (existingCategory == null)
                    {
                        db.Add(category);
                        db.SaveChanges();
                    }  
                        db.Add(HangHoaMoi);
                }
                // Save
                    db.SaveChanges();
            }
                return RedirectToAction("Index", "AdminCategory");
        }
                return View("Notfound","AdminCategory");
        }
    }
}