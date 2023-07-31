using AutoMapper;
using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, BasicProductDto>();
        CreateMap<PagedResultDto<Product>, PagedResultDto<ProductDto>>();
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<ProductCreateDto, Product>();
    }
}