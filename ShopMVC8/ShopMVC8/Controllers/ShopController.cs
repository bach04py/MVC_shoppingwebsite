using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopMVC8.ViewModels;
using ShopMVC8.Data;
using ShopMVC8.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopMVC8.Controllers
{
    [Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly Hshop2023Context db;
        public ShopController(Hshop2023Context context)
        {
            db = context;
        }

        [Route("")]
        public IActionResult Index(int? loai)
        {
           var Items = db.HangHoas.AsQueryable();
           if (loai.HasValue) {Items = Items.Where(p => p.MaLoai == loai.Value);}

            var result = Items.Select(p => new HangHoaVM 
        { 
            MaHh = p.MaHh,
            TenHh = p.TenHh ,
            DonGia = p.DonGia ?? 0,
            Hinh = p.Hinh ?? "",
            TenLoai = p.MaLoaiNavigation.TenLoai
         });
           return View("Shop",result);
        }

        [HttpPost]
        public IActionResult Sort(int fromPrice, int toPrice)
        {
        var sortedItems = db.HangHoas
            .Where(p => p.DonGia >= fromPrice && p.DonGia <= toPrice)
            .OrderBy(p => p.DonGia)
            .Select(p => new HangHoaVM
        {
            MaHh = p.MaHh,
            TenHh = p.TenHh,
            DonGia = p.DonGia ?? 0,
            Hinh = p.Hinh ?? "",
            TenLoai = p.MaLoaiNavigation.TenLoai
        });
            return View("Shop", sortedItems);
        }

        [Route("search")]
        public IActionResult Search(string? query)
        {
            var Items = db.HangHoas.AsQueryable();
            if (query != null)
            {
                Items = Items.Where(p => p.TenHh.Contains(query));
            }
            var result = Items.Select(p => new HangHoaVM 
            { 
            MaHh = p.MaHh,
            TenHh = p.TenHh ,
            DonGia = p.DonGia ?? 0,
            Hinh = p.Hinh ?? "",
            TenLoai = p.MaLoaiNavigation.TenLoai
            });
           return View("Shop",result);
        }
        
        [Route("Detail/{id}")]
        public IActionResult Detail(int? id)
        {
            var data = db.HangHoas
            .Include(p => p.MaLoaiNavigation)
            .SingleOrDefault(p => p.MaHh ==id);
            if (data == null) 
            {
                return View("Detail");
            }
            var result = new ChiTietHangHoaVM 
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                Hinh = data.Hinh ?? "",
                DiemDanhGia = 5,
                TenLoai = data.MaLoaiNavigation.TenLoai,

            };
            return View("Detail",result);
        }
    }
}