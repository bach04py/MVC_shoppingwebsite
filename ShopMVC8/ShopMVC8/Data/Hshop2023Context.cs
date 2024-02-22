using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ShopMVC8.Data;

public partial class Hshop2023Context : DbContext
{
    public Hshop2023Context()
    {
    }

    public Hshop2023Context(DbContextOptions<Hshop2023Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BanBe> BanBes { get; set; }

    public virtual DbSet<ChiTietHd> ChiTietHds { get; set; }

    public virtual DbSet<ChuDe> ChuDes { get; set; }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<GopY> Gopies { get; set; }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<HoiDap> HoiDaps { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<Loai> Loais { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhanCong> PhanCongs { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    public virtual DbSet<TrangWeb> TrangWebs { get; set; }

    public virtual DbSet<Vchitiethoadon> Vchitiethoadons { get; set; }

    public virtual DbSet<YeuThich> YeuThiches { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseMySql("server=localhost;database=Hshop2023;user=root;password=Maytinh1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<BanBe>(entity =>
        {
            entity.HasKey(e => e.MaBb).HasName("PRIMARY");

            entity.ToTable("BanBe");

            entity.HasIndex(e => e.MaKh, "FK_BanBe_KhachHang");

            entity.HasIndex(e => e.MaHh, "FK_QuangBa_HangHoa");

            entity.Property(e => e.MaBb).HasColumnName("MaBB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("text");
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.NgayGui).HasDefaultValueSql("'2023-03-05'");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.BanBes)
                .HasForeignKey(d => d.MaHh)
                .HasConstraintName("FK_QuangBa_HangHoa");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.BanBes)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BanBe_KhachHang");
        });

        modelBuilder.Entity<ChiTietHd>(entity =>
        {
            entity.HasKey(e => e.MaCt).HasName("PRIMARY");

            entity.ToTable("ChiTietHD");

            entity.HasIndex(e => e.MaHd, "FK_OrderDetails_Orders");

            entity.HasIndex(e => e.MaHh, "FK_OrderDetails_Products");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.DonGia)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.GiamGia)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.SoLuong).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.HasKey(e => e.MaCd).HasName("PRIMARY");

            entity.ToTable("ChuDe");

            entity.HasIndex(e => e.MaNv, "FK_ChuDe_NhanVien");

            entity.Property(e => e.MaCd).HasColumnName("MaCD");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.TenCd)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("TenCD");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.ChuDes)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_ChuDe_NhanVien");
        });

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<GopY>(entity =>
        {
            entity.HasKey(e => e.MaGy).HasName("PRIMARY");

            entity.ToTable("GopY");

            entity.HasIndex(e => e.MaCd, "FK_GopY_ChuDe");

            entity.Property(e => e.MaGy)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaGY");
            entity.Property(e => e.CanTraLoi).HasDefaultValueSql("'0'");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MaCd).HasColumnName("MaCD");
            entity.Property(e => e.NgayGy)
                .HasDefaultValueSql("'2023-03-05'")
                .HasColumnName("NgayGY");
            entity.Property(e => e.NgayTl).HasColumnName("NgayTL");
            entity.Property(e => e.NoiDung).HasColumnType("text");
            entity.Property(e => e.TraLoi)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.MaCdNavigation).WithMany(p => p.Gopies)
                .HasForeignKey(d => d.MaCd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GopY_ChuDe");
        });

        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.MaHh).HasName("PRIMARY");

            entity.ToTable("HangHoa");

            entity.HasIndex(e => e.MaLoai, "FK_Products_Categories");

            entity.HasIndex(e => e.MaNcc, "FK_Products_Suppliers");

            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.DonGia)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.GiamGia)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaNCC")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.MoTa)
                .HasColumnType("text")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.MoTaDonVi)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.NgaySx)
                .HasDefaultValueSql("'2023-03-05'")
                .HasColumnName("NgaySX");
            entity.Property(e => e.SoLanXem).HasDefaultValueSql("'0'");
            entity.Property(e => e.TenAlias)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.TenHh)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("TenHH")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PRIMARY");

            entity.ToTable("HoaDon");

            entity.HasIndex(e => e.MaNv, "FK_HoaDon_NhanVien");

            entity.HasIndex(e => e.MaTrangThai, "FK_HoaDon_TrangThai");

            entity.HasIndex(e => e.MaKh, "FK_Orders_Customers");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.CachThanhToan)
                .HasMaxLength(255)
                .HasDefaultValueSql("'Cash'");
            entity.Property(e => e.CachVanChuyen)
                .HasMaxLength(255)
                .HasDefaultValueSql("'Airline'");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.GhiChu)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.MaTrangThai).HasDefaultValueSql("'0'");
            entity.Property(e => e.NgayCan).HasDefaultValueSql("'2023-03-05'");
            entity.Property(e => e.NgayDat).HasDefaultValueSql("'2023-03-05'");
            entity.Property(e => e.NgayGiao).HasDefaultValueSql("'1900-01-01'");
            entity.Property(e => e.PhiVanChuyen)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_HoaDon_NhanVien");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaTrangThai)
                .HasConstraintName("FK_HoaDon_TrangThai");
        });

        modelBuilder.Entity<HoiDap>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PRIMARY");

            entity.ToTable("HoiDap");

            entity.HasIndex(e => e.MaNv, "FK_HoiDap_NhanVien");

            entity.Property(e => e.MaHd)
                .ValueGeneratedNever()
                .HasColumnName("MaHD");
            entity.Property(e => e.CauHoi)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayDua).HasDefaultValueSql("'2023-03-05'");
            entity.Property(e => e.TraLoi)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoiDaps)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoiDap_NhanVien");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PRIMARY");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.DienThoai)
                .HasMaxLength(24)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.GioiTinh).HasDefaultValueSql("'0'");
            entity.Property(e => e.HieuLuc).HasDefaultValueSql("'0'");
            entity.Property(e => e.Hinh)
                .HasMaxLength(255)
                .HasDefaultValueSql("'Photo.gif'");
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.NgaySinh).HasDefaultValueSql("'2023-03-05'");
            entity.Property(e => e.RandomKey)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.VaiTro).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<Loai>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PRIMARY");

            entity.ToTable("Loai");

            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.TenLoaiAlias)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PRIMARY");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaNCC")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Logo)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.NguoiLienLac)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.TenCongTy)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PRIMARY");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<PhanCong>(entity =>
        {
            entity.HasKey(e => e.MaPc).HasName("PRIMARY");

            entity.ToTable("PhanCong");

            entity.HasIndex(e => e.MaNv, "FK_PhanCong_NhanVien");

            entity.HasIndex(e => e.MaPb, "FK_PhanCong_PhongBan");

            entity.Property(e => e.MaPc).HasColumnName("MaPC");
            entity.Property(e => e.HieuLuc).HasColumnType("bit(1)");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.MaPb)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("MaPB");
            entity.Property(e => e.NgayPc)
                .HasColumnType("datetime")
                .HasColumnName("NgayPC");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanCong_NhanVien");

            entity.HasOne(d => d.MaPbNavigation).WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.MaPb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanCong_PhongBan");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.MaPq).HasName("PRIMARY");

            entity.ToTable("PhanQuyen");

            entity.HasIndex(e => e.MaPb, "FK_PhanQuyen_PhongBan");

            entity.HasIndex(e => e.MaTrang, "FK_PhanQuyen_TrangWeb");

            entity.Property(e => e.MaPq).HasColumnName("MaPQ");
            entity.Property(e => e.MaPb)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("MaPB");
            entity.Property(e => e.Sua).HasDefaultValueSql("'0'");
            entity.Property(e => e.Them).HasDefaultValueSql("'0'");
            entity.Property(e => e.Xem).HasDefaultValueSql("'0'");
            entity.Property(e => e.Xoa).HasDefaultValueSql("'0'");

            entity.HasOne(d => d.MaPbNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaPb)
                .HasConstraintName("FK_PhanQuyen_PhongBan");

            entity.HasOne(d => d.MaTrangNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaTrang)
                .HasConstraintName("FK_PhanQuyen_TrangWeb");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.MaPb).HasName("PRIMARY");

            entity.ToTable("PhongBan");

            entity.Property(e => e.MaPb)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("MaPB");
            entity.Property(e => e.TenPb)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("TenPB");
            entity.Property(e => e.ThongTin).HasColumnType("text");
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PRIMARY");

            entity.ToTable("TrangThai");

            entity.Property(e => e.MaTrangThai).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenTrangThai)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<TrangWeb>(entity =>
        {
            entity.HasKey(e => e.MaTrang).HasName("PRIMARY");

            entity.ToTable("TrangWeb");

            entity.Property(e => e.TenTrang).HasMaxLength(50);
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Vchitiethoadon>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vchitiethoadon");

            entity.Property(e => e.DonGia)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.GiamGia)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.SoLuong).HasDefaultValueSql("'1'");
            entity.Property(e => e.TenHh)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("TenHH")
                .UseCollation("utf8mb4_unicode_ci");
        });

        modelBuilder.Entity<YeuThich>(entity =>
        {
            entity.HasKey(e => e.MaYt).HasName("PRIMARY");

            entity.ToTable("YeuThich");

            entity.HasIndex(e => e.MaKh, "FK_Favorites_Customers");

            entity.HasIndex(e => e.MaHh, "FK_YeuThich_HangHoa");

            entity.Property(e => e.MaYt).HasColumnName("MaYT");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.NgayChon).HasColumnType("datetime");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.YeuThiches)
                .HasForeignKey(d => d.MaHh)
                .HasConstraintName("FK_YeuThich_HangHoa");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.YeuThiches)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_Favorites_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
