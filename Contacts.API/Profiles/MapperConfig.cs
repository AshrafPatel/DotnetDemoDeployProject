using AutoMapper;
using Contacts.Core.Entities;
using Contacts.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Services.Profiles
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}
