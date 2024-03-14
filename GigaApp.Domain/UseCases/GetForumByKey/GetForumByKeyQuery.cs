using GigaApp.Domain.Models;
using MediatR;

namespace GigaApp.Domain.UseCases.GetForumByKey;

public record GetForumByKeyQuery(Guid key) : IRequest<Forum>;