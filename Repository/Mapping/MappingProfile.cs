using AutoMapper;
using Domain.Models;
using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entities to Models mapping
            CreateMap<ApplicationUser, UserModel>();
        }
    }
}
