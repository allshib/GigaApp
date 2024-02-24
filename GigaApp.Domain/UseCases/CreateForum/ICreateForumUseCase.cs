using GigaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.CreateForum
{
    /// <summary>
    /// Use case создания форума
    /// </summary>
    public interface ICreateForumUseCase
    {

        /// <summary>
        /// Создать форум
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Forum> Execute(CreateForumCommand command, CancellationToken cancellationToken);
    }
}
