using API.Dtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<AddProductDto, Product>()
                .ForMember(d => d.ProductBrandId, o => o.MapFrom(s => s.ProductBrandId))
                .ForMember(d => d.ProductTypeId, o => o.MapFrom(s => s.ProductTypeId));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(d => d.ProductBrandId, o => o.MapFrom(s => s.ProductBrandId))
                .ForMember(d => d.ProductTypeId, o => o.MapFrom(s => s.ProductTypeId));
        }
    }
}
