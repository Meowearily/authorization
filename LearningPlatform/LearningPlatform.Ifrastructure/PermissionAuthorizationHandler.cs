﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningPlatform.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Security;
using LearningPlatform.Application.Interfaces.Auth;

namespace LearningPlatform.Infrastructure;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.UserId);

        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        var permissions = await permissionService.GetPermissionAsync(id);

        if(permissions.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}
