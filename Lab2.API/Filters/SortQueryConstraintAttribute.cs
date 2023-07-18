using Lab2.API.Dtos.Shared;
using Lab2.API.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lab2.API.Filters;

public class SortQueryConstraintAttribute : ActionFilterAttribute
{
    public string? Fields { get; set; }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!string.IsNullOrWhiteSpace(Fields) && context.HttpContext.Request.Query.ContainsKey(nameof(PagingRequestDto.Sorting)))
        {
            var sortingQueryValue = context.HttpContext.Request.Query["Sorting"].ToString().ToLower();
            if (!string.IsNullOrWhiteSpace(sortingQueryValue))
            {
                var values = sortingQueryValue.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
                var allowedSortingFields = Fields.ToLower().Split(new char[] { ',' }, StringSplitOptions.TrimEntries);

                foreach (var value in values)
                {
                    var splitedValue = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (!allowedSortingFields.Contains(splitedValue[0]))
                    {
                        throw new BadRequestException($"Only sorting in '{Fields}' fields is allowed!");
                    }    
                    
                    if (splitedValue.Length > 1 && splitedValue[1] != "asc" && splitedValue[1] != "desc")
                    {
                        throw new BadRequestException($"Orderby param only accepts 'asc' or 'desc'!");
                    }    
                }    
            }    
        }    
    }
}
