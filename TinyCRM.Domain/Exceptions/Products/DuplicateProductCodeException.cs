using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Exceptions.Products;

public class DuplicateProductCodeException : ResourceAlreadyExistException
{
    public DuplicateProductCodeException(string code) : base(nameof(Product), nameof(Product.Code), code, ErrorCodes.ProductCodeAlreadyExists)
    {
    }
}