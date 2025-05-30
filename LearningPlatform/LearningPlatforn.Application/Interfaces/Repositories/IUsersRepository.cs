﻿using LearningPlatform.Core.Enums;
using LearningPlatform.Core.Models;

namespace LearningPlatform.Application.Interfaces.Repositories;
public interface IUsersRepository
{
    Task Add(User user);
    Task<User> GetByEmail(string email);

    Task<HashSet<Permission>> GetUserPermissions(Guid userId);
}
