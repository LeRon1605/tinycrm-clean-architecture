using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IProductService : IService<Product, ProductDto, ProductCreateDto, ProductUpdateDto>
{
}
