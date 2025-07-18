﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using GigaApp.XAF.Module.BusinessObjects;
using GigaApp.XAF.Module.ServiceClasses;
using Microsoft.Extensions.DependencyInjection;

namespace GigaApp.XAF.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class XAFModule : ModuleBase {
    public XAFModule() {
		// 
		// XAFModule
		// 
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));

    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        NonPersistentObjectSpace.UseKeyComparisonToDetermineIdentity = true;
        NonPersistentObjectSpace.AutoSetModifiedOnObjectChangeByDefault = true;
        application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
    }

    private void Application_ObjectSpaceCreated(object? sender, ObjectSpaceCreatedEventArgs e)
    {
        if (e.ObjectSpace is NonPersistentObjectSpace)
        {
            NonPersistentObjectSpace npos = (NonPersistentObjectSpace)e.ObjectSpace;
            var app = sender as XafApplication;
            npos.AutoDisposeAdditionalObjectSpaces = true;
            var types = new Type[] { typeof(Forum) };
            var map = new ObjectMap(npos, types);
            new TransientNonPersistentObjectAdapter(npos, map, app.ServiceProvider.GetRequiredService<NonPersistentStorageBase>());
        }
    }

}
