namespace ShopMVC8.ViewModels
{
    public class CartItem
    {
        public int MaHh { get; set; }
        public string? Hinh { get; set; }
        public string? TenHh { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => (double)(SoLuong * DonGia);
    }
}