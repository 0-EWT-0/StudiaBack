﻿namespace Application.DTOS.Responses
{
    public record LoginResponse(bool Flag, string Message = null!, string Token = null!);
}
