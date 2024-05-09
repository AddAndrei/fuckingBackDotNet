﻿using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtProvider
{
    string GenerateJwtToken(User user);
}
