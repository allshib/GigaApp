using MediatR;

namespace GigaApp.Domain.UseCases.DeleteForumByKey;

public record DeleteForumByKeyCommand(Guid ForumId) : IRequest;
