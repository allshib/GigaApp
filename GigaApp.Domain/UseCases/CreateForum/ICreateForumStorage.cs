using FluentValidation;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.CreateForum
{
    public interface ICreateForumStorage
    {
        Task<Forum?> Create(string title, CancellationToken cancellationToken);
    }

    
}
