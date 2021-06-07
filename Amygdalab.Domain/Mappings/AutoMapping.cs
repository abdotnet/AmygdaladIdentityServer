﻿using Amygdalab.Core.Contracts.Request;
using Amygdalab.Core.Contracts.Response;
using Amygdalab.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ProductResponse, Product > ().ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();
            
        }
    }
}
