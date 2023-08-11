using AutoMapper;
using Bechelor.Core.Common;
using Hello_Bechelor.Areas.Admin.Models;

namespace Hello_Bechelor.Areas.Admin.Mappings
{
    public class TanentViewModelProfile : Profile
    {
        public TanentViewModelProfile()
        {
            CreateMap<Tanent, TanentViewModel>().ReverseMap();
        }
    }
}
