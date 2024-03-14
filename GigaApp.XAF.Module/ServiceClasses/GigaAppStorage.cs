using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System.Collections;
using AutoMapper;
using GigaApp.XAF.Module.BusinessObjects;
using GigaApp.Domain.UseCases.GetForums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.Execution;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.UseCases.GetForumByKey;
using GigaApp.XAF.Module.UseCaseExtensions;
using MediatR;
using System.Linq;
using GigaApp.Domain.UseCases.CreateForum;

namespace GigaApp.XAF.Module.ServiceClasses;

public class GigaAppStorage(IMediator useCase, IMapper mapper) : NonPersistentStorageBase
{
    public override object GetObjectByKey(Type objectType, object key)
    {
        if (objectType == typeof(Forum))
            return useCase.GetForumByKey(key, mapper);
        

        return default;
    }

    public override IEnumerable GetObjects(Type objectType, CriteriaOperator criteria, IList<SortProperty> sorting)
    {
        if (objectType == typeof(Forum))
            return useCase.GetForums(mapper);

        return Enumerable.Empty<object>();
    }

    public override void SaveObjects(ICollection<NonPersistentObjectBase> toInsert, ICollection<NonPersistentObjectBase> toUpdate, ICollection<NonPersistentObjectBase> toDelete)
    {
        base.SaveObjects(toInsert, toUpdate, toDelete);

        if (toInsert.Any(x => x is Forum))
        {
            var forums = toInsert.OfType<Forum>();

            foreach (var forum in forums)
            {
                var task = Task.Run(() => useCase
                    .Send(new CreateForumCommand(forum.Title), CancellationToken.None));

                mapper.Map<Forum>(task.Result);
            }
        }


    }
}