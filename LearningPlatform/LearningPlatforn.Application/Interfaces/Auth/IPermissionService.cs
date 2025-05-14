using LearningPlatform.Application.Interfaces.Repositories;
using LearningPlatform.Core.Enums;
using LearningPlatform.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Interfaces.Auth;
public interface IPermissionService
{
    public Task<HashSet<Permission>> GetPermissionAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    //Task<HashSet<Permission>> GetPermissionAsync(Guid userId);

    //public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
    //{
    //    return _usersRepository.GetUserPermissions(userId);
    //}
}
