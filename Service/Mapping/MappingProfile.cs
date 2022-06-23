using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entities to Models mapping
            CreateMap<ApplicationUser, UserModel>();
            // Models to Entities mapping
            CreateMap<DeclarationModel, Declaration>();
            CreateMap<Declaration, DeclarationModel>();

        }
    }
}
