using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Contacts.Data.Models;
using Contacts.DTO;

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
