﻿using LearningPlatform.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Infrastructure;
public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;

}
