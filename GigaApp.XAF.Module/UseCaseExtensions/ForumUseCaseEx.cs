using AutoMapper;
using DevExpress.XtraRichEdit.Commands;
using GigaApp.Domain.UseCases.CreateForum;
using GigaApp.Domain.UseCases.DeleteForumByKey;
using GigaApp.Domain.UseCases.GetForumByKey;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases.UpdateForumUseCase;
using GigaApp.XAF.Module.BusinessObjects;
using MediatR;

namespace GigaApp.XAF.Module.UseCaseExtensions;

public static class ForumUseCaseEx
{
    public static Forum GetForumByKey(this IMediator useCase, object key, IMapper mapper)
    {
        var id = (Guid)key;
        var task = Task
            .Run(() => useCase
                .Send(new GetForumByKeyQuery(id), CancellationToken.None));

        return mapper.Map<Forum>(task.Result);
    }

    public static IEnumerable<Forum> GetForums(this IMediator useCase, IMapper mapper)
    {
        var task = Task.Run(() => useCase
            .Send(new GetForumsQuery(), CancellationToken.None));

        return task.Result.Select(forum => mapper.Map<Forum>(forum));

    }

    public static void DeleteForum(this IMediator useCase,Guid forumId)
    {
        Task.Run(() => useCase
            .Send(new DeleteForumByKeyCommand(forumId), CancellationToken.None));

    }

    public static void UpdateForum(this IMediator useCase, Forum forum, IMapper mapper)
    {

        var domainForum = mapper.Map<GigaApp.Domain.Models.Forum>(forum);

        Task.Run(() => useCase
            .Send(new UpdateForumCommand(domainForum), CancellationToken.None));

    }

    public static void CreateForum(this IMediator useCase, Forum forum)
    {
        Task.Run(() => useCase
            .Send(new CreateForumCommand(forum.Title), CancellationToken.None));

    }
}