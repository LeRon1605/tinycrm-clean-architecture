using TinyCRM.Application.Dtos.Products;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Mapper;

public class ProductMapper : TinyCrmMapper
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