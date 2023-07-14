using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Services;

public class DealService : IDealService
{
    private readonly IDealRepository _dealRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILeadRepository _leadRepository;
    private readonly IProductRepository _productRepository;
    private readonly IRepository<DealLine, int> _dealLineRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DealService(
        IDealRepository dealRepository,
        IAccountRepository accountRepository,
        IProductRepository productRepository,
        IRepository<DealLine, int> dealLineRepository,
        ILeadRepository leadRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _dealRepository = dealRepository;   
        _accountRepository = accountRepository;
        _leadRepository = leadRepository;
        _productRepository = productRepository;
        _dealLineRepository = dealLineRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task DeleteAsync(int id)
    {
        // Get deal from db
        Deal deal = await _dealRepository.FindAsync(x => x.Id == id);

        // Check if deal exist
        if (deal == null)
        {
            throw new NotFoundException($"Deal with id '{id}' does not exist.");
        }

        // Only allow delete deal on open status
        if (deal.Status != DealStatus.Open)
        {
            throw new BadRequestException($"Can not delete deal which is on won or lost status");
        }

        _dealRepository.Delete(deal);
        await _unitOfWork.CommitAsync();
        
    }

    public async Task<PagedResultDto<DealDto>> GetAllAsync(DealFilterAndPagingRequestDto filterParam)
    {
        var data = await _dealRepository.GetListAsync(
                                                skip: (filterParam.Page - 1) * filterParam.Size,
                                                take: filterParam.Size,
                                                x => x.Title.Contains(filterParam.Title) &&
                                                     (filterParam.Status == null || x.Status == filterParam.Status)
                                           );
        var total = await _dealRepository.CountAsync(x => x.Title.Contains(filterParam.Title) && (filterParam.Status == null || x.Status == filterParam.Status));

        return new PagedResultDto<DealDto>()
        {
            Data = _mapper.Map<List<DealDto>>(data),
            Total = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public async Task<DealDto> GetAsync(int id)
    {
        Deal deal = await _dealRepository.FindAsync(x => x.Id == id);
        if (deal == null)
        {
            throw new NotFoundException($"Deal with id '{id}' does not exist.");
        }

        return _mapper.Map<DealDto>(deal);
    }

    public async Task<DealDto> UpdateAsync(int id, DealUpdateDto dealUpdateDto)
    {
        // Get deal from db
        Deal deal = await _dealRepository.FindAsync(x => x.Id == id);
        if (deal == null)
        {
            throw new NotFoundException($"Deal with id '{id}' does not exist.");
        }

        // Allow update description and source only for disqualified and qualified
        deal.Description = dealUpdateDto.Description;

        if (deal.Status == DealStatus.Open)
        {
            deal.Title = dealUpdateDto.Title;
            deal.EstimatedRevenue = dealUpdateDto.EstimatedRevenue;
        }

        // Update lead from db
        _dealRepository.Update(deal);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<DealDto>(deal);
    }

    public async Task<DealLineDto> AddLineAsync(int id, DealLineCreateDto productDealCreateDto)
    {
        // Check if deal exist
        Deal deal = await _dealRepository.FindByIdAsync(id);
        if (deal == null)
        {
            throw new NotFoundException($"Deal with id '{id}' does not exist.");
        }

        // Check if product exist
        Product product = await _productRepository.FindByIdAsync(productDealCreateDto.ProductId);
        if (product == null)
        {
            throw new NotFoundException($"Product with id '{productDealCreateDto.ProductId}' does not exist.");
        }

        DealLine line = _mapper.Map<DealLine>(productDealCreateDto);
        line.Deal = deal;
        line.Product = product;

        await _dealLineRepository.InsertAsync(line);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<DealLineDto>(line);
    }

    public async Task<IEnumerable<DealLineDto>> GetProductsInDealAsync(int id)
    {
        var deal = await _dealRepository.FindAsync(x => x.Id == id);
        if (deal == null)
        {
            throw new NotFoundException($"Deal with id '{id}' does not exist.");
        }

        return _mapper.Map<IEnumerable<DealLineDto>>(deal.Lines);
    }
}
