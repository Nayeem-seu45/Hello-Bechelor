using AutoMapper;
using Bechelor.Core.Common;
using Hello_Bechelor.Areas.Admin.Models;
using Hello_Bechelor.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Areas.Admin.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
            CreateMap<ApplicationRole, RoleViewModel>().ReverseMap();
            CreateMap<ApplicationUser, UserRolesViewModel>().ReverseMap();
        }
    }
}
