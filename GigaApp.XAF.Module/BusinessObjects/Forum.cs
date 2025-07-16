using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Data;

namespace GigaApp.XAF.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty(nameof(Title))]
    [XafDisplayName("Форум")]
    public class Forum : NonPersistentObjectBase
    {
        //private IObjectSpace objectSpace;
        public Forum()
        {

        }

        private Guid id;

        [Appearance("", Enabled = false, Criteria = "Not IsNewObject(This)")]
        [Key]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        private string? title;

        [XafDisplayName("Наименование"), ToolTip("Имя форума")]
        public string? Title
        {
            get { return title; }
            set { SetPropertyValue(nameof(Title), ref title, value); }
        }

    }
}