using Lab2.Domain.Entities;

namespace Lab2.API.Exceptions;

public class ProductCodeAlreadyExist : EntityConflictException
{
    public ProductCodeAlreadyExist(string value)
        : base(nameof(Product), nameof(Product.Code), value, ErrorCodes.ProductCodeAlreadyExists)
    {
    }
}