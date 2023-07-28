using Lab2.API.Authorization.Requirements;
using Lab2.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Lab2.API.Authorization.Handlers;

public class EditProfileAuthorizationHandler : AuthorizationHandler<EditProfileRequirement, User>
{
    private readonly ICurrentUser _currentUser;

    public EditProfileAuthorizationHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditProfileRequirement requirement, User resource)
    {
        if (_currentUser.IsInRole(AppRole.Admin) || _currentUser.Id == resource.Id)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}