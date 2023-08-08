using TinyCRM.Application.Dtos.Products;

namespace TinyCRM.Application.Services.Interfaces;

public interface IProductService : IService<Product, int, ProductDto, ProductCreateDto, ProductUpdateDto>
{
}