using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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