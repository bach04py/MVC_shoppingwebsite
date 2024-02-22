using AutoMapper;
using ShopMVC8.Data;
using ShopMVC8.ViewModels;
namespace ShopMVC8.Helper
{
public class AutomapperCategory : Profile
{
    public AutomapperCategory()
    {
        CreateMap<QuanliHangHoaVM, HangHoa>();
        CreateMap<QuanliHangHoaVM, Loai>()
        .ForMember(dest => dest.MaLoai, opt => opt.MapFrom(src => src.MaLoai))
        .ForMember(dest => dest.TenLoai, opt => opt.MapFrom(src => src.TenAlias));

        // Thêm các quy tắc ánh xạ khác nếu cần
    }
}
}