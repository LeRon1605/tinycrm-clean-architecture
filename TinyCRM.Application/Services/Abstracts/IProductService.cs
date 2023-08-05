using TinyCRM.Application.Dtos.Products;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Services.Abstracts;

public interface IProductService : IService<Product, int, ProductDto, ProductCreateDto, ProductUpdateDto>
{
}