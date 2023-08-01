using Lab2.API.Authorization.Requirements;
using Lab2.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Lab2.API.Authorization.Handlers;

public class EditProfileAuthorizationHandler : AuthorizationHandler<EditProfileRequirement, User>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditProfileRequirement requirement, User resource)
    {
        if (context.User.IsInRole(AppRole.Admin) || context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.Id)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}