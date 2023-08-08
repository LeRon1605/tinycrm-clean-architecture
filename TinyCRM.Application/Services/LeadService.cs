using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.Dtos.Leads;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Exceptions.Leads;
using TinyCRM.Domain.Specifications.Leads;

namespace TinyCRM.Application.Services;

public class LeadService : BaseService<Lead, int, LeadDto, LeadCreateDto, LeadUpdateDto>, ILeadService
{
    private readonly ILeadRepository _leadRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IDealRepository _dealRepository;

    public LeadService(
        IAccountRepository accountRepository,
        ILeadRepository leadRepository,
        IDealRepository dealRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, leadRepository, unitOfWork)
    {
        _leadRepository = leadRepository;
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
        if (!LeadSpecification.ProcessedLeadSpecification.IsSatisfiedBy(lead) && lead.CustomerId != leadUpdateDto.CustomerId)
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

        if (!LeadSpecification.ProcessedLeadSpecification.IsSatisfiedBy(lead))
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
        if (LeadSpecification.ProcessedLeadSpecification.IsSatisfiedBy(lead))
        {
            throw new LeadProcessedException(lead.Id, lead.Status);
        }

        return Task.FromResult(true);
    }

    public async Task<LeadStatisticDto> GetStatisticAsync()
    {
        return new LeadStatisticDto()
        {
            OpenLead = await _leadRepository.GetCountOpenLeadAsync(),
            DisqualifiedLead = await _leadRepository.GetCountDisqualifiedAsync(),
            QualifiedLead = await _leadRepository.GetCountQualifiedAsync(),
            AverageEstimatedRevenue = await _leadRepository.GetAverageEstimatedRevenueAsync()
        };
    }

    public async Task<PagedResultDto<LeadDto>> GetByAccountAsync(int accountId, LeadFilterAndPagingRequestDto filterParam)
    {
        await CheckAccountExistingAsync(accountId);

        var getPagedLeadForAccountSpecification = filterParam.ToSpecification().And(new LeadForAccountSpecification(accountId));
        return await GetPagedAsync(getPagedLeadForAccountSpecification);
    }

    public async Task<DealDto> QualifyAsync(int id)
    {
        // Check if we can permit to perform qualify lead
        var lead = await CheckPerformQualifyOrDisqualifyAsync(id);

        // Update lead status to qualified
        lead.Status = LeadStatus.Qualified;
        lead.EndedDate = DateTime.Now;
        _leadRepository.Update(lead);

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
        UpdateLeadToDisqualified(lead, disqualifiedLeadCreateDto);

        _leadRepository.Update(lead);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<LeadDto>(lead);
    }

    private void UpdateLeadToDisqualified(Lead lead, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto)
    {
        lead.Status = LeadStatus.Disqualified;
        lead.Reason = disqualifiedLeadCreateDto.Reason;
        lead.ReasonDescription = disqualifiedLeadCreateDto.ReasonDescription;
        lead.EndedDate = DateTime.Now;
    }

    private async Task<Lead> CheckPerformQualifyOrDisqualifyAsync(int id)
    {
        // Fetch lead from db
        var lead = await _leadRepository.FindByIdAsync(id);
        if (lead == null)
        {
            throw new ResourceNotFoundException(nameof(Lead), id);
        }

        // Check if lead has already qualified or disqualified yet
        if (!LeadSpecification.ProcessedLeadSpecification.IsSatisfiedBy(lead))
        {
            throw new LeadProcessedException(id, lead.Status);
        }

        return lead;
    }

    private async Task<bool> CheckAccountExistingAsync(int accountId)
    {
        var isCustomerExisting = await _accountRepository.IsExistingAsync(accountId);
        if (!isCustomerExisting)
        {
            throw new ResourceNotFoundException(nameof(Account), accountId);
        }

        return true;
    }
}