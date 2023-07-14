using AutoMapper;
using EntityFramework.Exceptions.Common;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Services;

public class LeadService : ILeadService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILeadRepository _leadRepository;
    private readonly IDealRepository _dealRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public LeadService(
        IAccountRepository accountRepository,
        ILeadRepository leadRepository,
        IDealRepository dealRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _accountRepository = accountRepository; 
        _leadRepository = leadRepository;
        _dealRepository = dealRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LeadDto> CreateAsync(LeadCreateDto leadCreateDto)
    {
        // Create lead entity from dto
        Lead lead = _mapper.Map<Lead>(leadCreateDto);

        try
        {
            // Insert to db
            await _leadRepository.InsertAsync(lead);
            await _unitOfWork.CommitAsync();
        } 
        catch (ReferenceConstraintException) 
        {
            // Catch reference constaint error on account id column and throw exception
            throw new NotFoundException($"Account with id '{leadCreateDto.CustomerId}' does not exist.");
        }

        return _mapper.Map<LeadDto>(lead);
    }

    public async Task DeleteAsync(int id)
    {
        // Get lead from db
        Lead lead = await _leadRepository.FindAsync(x => x.Id == id);
        if (lead == null)
        {
            // Throw exception if lead does not exist
            throw new NotFoundException($"Lead with Id '{id}' does not exist.");
        }

        // Throw exception if lead is on qualified or disqualified status
        if (lead.Status == LeadStatus.Disqualified || lead.Status == LeadStatus.Qualified)
        {
            throw new BadRequestException($"Can not delete lead which is on qualified or disqualified status.");
        }

        // Delete lead from db
        _leadRepository.Delete(lead);
        await _unitOfWork.CommitAsync();
    }

    public async Task<PagedResultDto<LeadDto>> GetAllAsync(LeadFilterAndPagingRequestDto filterParam)
    {
        var data = await _leadRepository.GetListAsync(
                                                skip: (filterParam.Page - 1) * filterParam.Size, 
                                                take: filterParam.Size, 
                                                x => x.Title.Contains(filterParam.Title) &&
                                                     (filterParam.Status == null || x.Status == filterParam.Status)
                                           );
        var total = await _leadRepository.CountAsync(x => x.Title.Contains(filterParam.Title) && (filterParam.Status == null || x.Status == filterParam.Status));

        return new PagedResultDto<LeadDto>()
        {
            Data = _mapper.Map<List<LeadDto>>(data),
            Total = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public async Task<LeadDto> GetAsync(int id)
    {
        // Get lead from db
        Lead lead = await _leadRepository.FindAsync(x => x.Id == id);
        if (lead == null)
        {
            // Throw exception if lead does not exist
            throw new NotFoundException($"Lead with id '{id}' does not exist.");
        }

        return _mapper.Map<LeadDto>(lead);
    }

    public async Task<LeadDto> UpdateAsync(int id, LeadUpdateDto leadUpdateDto)
    {
        // Get lead from db
        Lead lead = await _leadRepository.FindAsync(x => x.Id == id);
        if (lead == null)
        {
            throw new NotFoundException($"Lead with id '{id}' does not exist.");
        }

        // Allow update description and source only for disqualified and qualified
        lead.Title = leadUpdateDto.Title;
        lead.Source = leadUpdateDto.Source;

        if (lead.Status != LeadStatus.Qualified || lead.Status != LeadStatus.Disqualified)
        {
            lead.Status = leadUpdateDto.Status;
            lead.CustomerId = leadUpdateDto.CustomerId;
            lead.Description = leadUpdateDto.Description;
            lead.EstimatedRevenue = leadUpdateDto.EstimatedRevenue;
        }

        try
        {
            // Update lead from db
            _leadRepository.Update(lead);
            await _unitOfWork.CommitAsync();
        }
        catch (ReferenceConstraintException)
        {
            // Catch reference constaint error on account id column and throw exception
            throw new NotFoundException($"Account with id '{leadUpdateDto.CustomerId}' does not exist.");
        }

        return _mapper.Map<LeadDto>(lead);
    }

    public async Task<IEnumerable<LeadDto>> GetLeadsOfAccountAsync(int accountId)
    {
        // Load account with lead
        Account account = await _accountRepository.FindDetailAsync(x => x.Id == accountId);
        if (account == null)
        {
            throw new NotFoundException($"Account with id '{accountId}' does not exist.");
        }

        return _mapper.Map<IEnumerable<LeadDto>>(account.Leads);
    }

    public async Task<DealDto> QualifyAsync(int id)
    {
        // Check if we can permit to perform qualify lead
        Lead lead = await CheckPerformQualifyOrDisqualifyAsync(id);

        // Update lead status to qualified
        lead.Status = LeadStatus.Qualified;
        lead.EndedDate = DateTime.Now;
        _leadRepository.Update(lead);

        // Create new deal from lead
        var deal = new Deal()
        {
            Title = lead.Title,
            EstimatedRevenue = lead.EstimatedRevenue,
            Description = string.Empty,
            Status = DealStatus.Open,
            Lead = lead
        };
        await _dealRepository.InsertAsync(deal);

        // Commit to db
        await _unitOfWork.CommitAsync();
        return _mapper.Map<DealDto>(deal);
    }

    public async Task<LeadDto> DisqualifyAsync(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto)
    {
        // Check if we can permit to perform disqualify lead
        Lead lead = await CheckPerformQualifyOrDisqualifyAsync(id);

        // Update lead to disqualified status
        lead.Status = LeadStatus.Disqualified;
        lead.Reason = disqualifiedLeadCreateDto.Reason;
        lead.ReasonDescription = disqualifiedLeadCreateDto.ReasonDescription;
        lead.EndedDate = DateTime.Now;

        // Commit to db
        _leadRepository.Update(lead);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<LeadDto>(lead);
    }

    private async Task<Lead> CheckPerformQualifyOrDisqualifyAsync(int id)
    {
        // Fetch lead from db
        Lead lead = await _leadRepository.FindAsync(x => x.Id == id);
        if (lead == null)
        {
            // Throw exception if lead does not exist
            throw new NotFoundException($"Lead with id '{id}' does not exist.");
        }

        // Check if lead has already qualified or disqualified yet
        if (lead.Status == LeadStatus.Qualified || lead.Status == LeadStatus.Disqualified)
        {
            throw new BadRequestException($"Lead with id '{lead.Id}' has already {lead.Status}");
        }

        return lead;
    }
}
