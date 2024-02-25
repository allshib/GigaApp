using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.SignOn
{
    public interface ISignOnStorage
    {
        Task<Guid> CreateUser(string login, byte[] salt, byte[] hash, CancellationToken cancellationToken);
    }
}
