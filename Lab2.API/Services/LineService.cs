using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class LineService : ILineService
{
    private readonly IRepository<DealLine, int> _dealLineRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public LineService(
        IRepository<DealLine, int> dealLineRepository, 
        IUnitOfWork unitOfWork, 
        IProductRepository productRepository,
        IMapper mapper)
    {
        _dealLineRepository = dealLineRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task DeleteAsync(int id)
    {
        DealLine line = await _dealLineRepository.FindByIdAsync(id);
        if (line == null)
        {
            throw new NotFoundException($"Line with id '{id}' does not exist");
        }

        _dealLineRepository.Delete(line);
        await _unitOfWork.CommitAsync();
    }

    public async Task<DealLineDto> UpdateAsync(int id, DealLineUpdateDto dealLineUpdateDto)
    {
        DealLine line = await _dealLineRepository.FindByIdAsync(id);
        if (line == null)
        {
            throw new NotFoundException($"Line with id '{id}' does not exist.");
        }

        if (dealLineUpdateDto.ProductId != line.ProductId)
        {
            Product product = await _productRepository.FindByIdAsync(line.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Product with id '{line.ProductId}' does not exist.");
            }

            line.Product = product;
        }

        line.PricePerUnit = dealLineUpdateDto.PricePerUnit;
        line.Quantity = dealLineUpdateDto.Quantity;

        _dealLineRepository.Update(line);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<DealLineDto>(line);
    }
}
