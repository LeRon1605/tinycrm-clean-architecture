using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.API.Extensions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class LeadService : BaseService<Lead, int, LeadDto, LeadCreateDto, LeadUpdateDto>, ILeadService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDealRepository _dealRepository;

    public LeadService(
        IAccountRepository accountRepository,
        ILeadRepository leadRepository,
        IDealRepository dealRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, leadRepository, unitOfWork)
    {
        _accountRepository = accountRepository;
        _dealRepository = dealRepository;

        _includePropsOnGet = nameof(Lead.Customer);
    }

    protected override async Task<bool> IsValidOnInsertAsync(LeadCreateDto leadCreateDto)
    {
        return await CheckAccountExistingAsync(leadCreateDto.CustomerId);
    }

    protected override async Task<bool> IsValidOnUpdateAsync(Lead lead, LeadUpdateDto leadUpdateDto)
    {
        if (lead.Status != LeadStatus.Qualified || lead.Status != LeadStatus.Disqualified && lead.CustomerId != leadUpdateDto.CustomerId)
        {
            return await CheckAccountExistingAsync(leadUpdateDto.CustomerId);
        }

        return true;
    }

    protected override Lead UpdateEntity(Lead lead, LeadUpdateDto leadUpdateDto)
    {
        // Allow update description and source only for disqualified and qualified
        lead.Description = leadUpdateDto.Description;
        lead.Source = leadUpdateDto.Source;

        if (lead.Status != LeadStatus.Qualified && lead.Status != LeadStatus.Disqualified)
        {
            lead.Title = leadUpdateDto.Title;
            lead.Status = leadUpdateDto.Status;
            lead.CustomerId = leadUpdateDto.CustomerId;
            lead.EstimatedRevenue = leadUpdateDto.EstimatedRevenue;
        }

        return lead;
    }

    protected override Task<bool> IsValidOnDeleteAsync(Lead lead)
    {
        // Can not delete lead which is on qualified or disqualified status
        if (lead.Status == LeadStatus.Disqualified || lead.Status == LeadStatus.Qualified)
        {
            throw new InvalidRemoveLeadException(lead.Id);
        }

        return Task.FromResult(true);
    }

    public async Task<PagedResultDto<LeadDto>> GetLeadsOfAccountAsync(int accountId, LeadFilterAndPagingRequestDto filterParam)
    {
        await CheckAccountExistingAsync(accountId);

        return await GetPagedAsync(skip: (filterParam.Page - 1) * filterParam.Size,
                                   take: filterParam.Size,
                                   expression: filterParam.ToExpression().JoinWith(x => x.CustomerId == accountId),
                                   sorting: filterParam.BuildSortingParam());
    }

    public async Task<LeadStatisticDto> GetStatisticAsync()
    {
        return new LeadStatisticDto()
        {
            OpenLead = await _repository.GetCountAsync(x => x.Status == LeadStatus.Open),
            DisqualifiedLead = await _repository.GetCountAsync(x => x.Status == LeadStatus.Disqualified),
            QualifiedLead = await _repository.GetCountAsync(x => x.Status == LeadStatus.Qualified),
            AverageEstimatedRevenue = await _repository.GetAverageAsync(x => x.EstimatedRevenue)
        };
    }

    public async Task<DealDto> QualifyAsync(int id)
    {
        // Check if we can permit to perform qualify lead
        var lead = await CheckPerformQualifyOrDisqualifyAsync(id);

        // Update lead status to qualified
        lead.Status = LeadStatus.Qualified;
        lead.EndedDate = DateTime.Now;
        _repository.Update(lead);

        // Create new deal from lead
        var deal = new Deal(lead);
        await _dealRepository.InsertAsync(deal);

        // Commit to db
        await _unitOfWork.CommitAsync();
        return _mapper.Map<DealDto>(deal);
    }

    public async Task<LeadDto> DisqualifyAsync(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto)
    {
        // Check if we can permit to perform disqualify lead
        var lead = await CheckPerformQualifyOrDisqualifyAsync(id);

        // Update lead to disqualified status
        UpdateLeadToDisqualifed(lead, disqualifiedLeadCreateDto);

        _repository.Update(lead);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<LeadDto>(lead);
    }

    private void UpdateLeadToDisqualifed(Lead lead, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto)
    {
        lead.Status = LeadStatus.Disqualified;
        lead.Reason = disqualifiedLeadCreateDto.Reason;
        lead.ReasonDescription = disqualifiedLeadCreateDto.ReasonDescription;
        lead.EndedDate = DateTime.Now;
    }

    private async Task<Lead> CheckPerformQualifyOrDisqualifyAsync(int id)
    {
        // Fetch lead from db
        var lead = await _repository.FindAsync(x => x.Id == id);
        if (lead == null)
        {
            throw new EntityNotFoundException(nameof(Lead), id);
        }

        // Check if lead has already qualified or disqualified yet
        if (lead.Status == LeadStatus.Qualified || lead.Status == LeadStatus.Disqualified)
        {
            throw new InvalidQualifyOrDisqualifyLeadException(id, lead.Status);
        }

        return lead;
    }

    private async Task<bool> CheckAccountExistingAsync(int accountId)
    {
        var isCustomerExisting = await _accountRepository.AnyAsync(x => x.Id == accountId);
        if (!isCustomerExisting)
        {
            throw new EntityNotFoundException(nameof(Account), accountId);
        }

        return true;
    }
}