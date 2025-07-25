﻿namespace GigaApp.Domain.Identity
{
    /// <summary>
    /// Провайдер авторизации
    /// Содержит текущего пользователя
    /// </summary>
    public interface IIdentityProvider
    {
        IIdentity Current { get; set; }
    }

    //public interface IIdentitySetter
    //{
    //    IIdentity Current { set; }
    //}
}
