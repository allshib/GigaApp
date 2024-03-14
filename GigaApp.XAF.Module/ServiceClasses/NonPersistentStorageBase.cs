using System.Collections;
using DevExpress.Data.Filtering;
using GigaApp.XAF.Module.BusinessObjects;

namespace GigaApp.XAF.Module.ServiceClasses;

public abstract class NonPersistentStorageBase
{
    public abstract object GetObjectByKey(Type objectType, object key );
    public abstract IEnumerable GetObjects(Type objectType, CriteriaOperator criteria, IList<DevExpress.Xpo.SortProperty> sorting);

    public virtual void SaveObjects(ICollection<NonPersistentObjectBase> toInsert, ICollection<NonPersistentObjectBase> toUpdate, ICollection<NonPersistentObjectBase> toDelete)
    {
        
    }
}