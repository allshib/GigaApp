﻿using GigaApp.Domain.Authentication;

namespace GigaApp.Domain.UseCases.SignIn
{
    public interface ISignInStorage
    {
        Task<RecognizedUser> FindUser(string login, CancellationToken cancellationToken);
        Task<Guid> CreateSession(Guid userId,DateTimeOffset expirationMoment, CancellationToken cancellationToken);
    }
}
