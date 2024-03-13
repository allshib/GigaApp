//using DevExpress.ExpressApp.EFCore.Updating;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
//using DevExpress.Persistent.BaseImpl.EF;
//using DevExpress.ExpressApp.Design;
//using DevExpress.ExpressApp.EFCore.DesignTime;

//namespace GigaApp.XAF.Module.BusinessObjects;

//// This code allows our Model Editor to get relevant EF Core metadata at design time.
//// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
//public class XAFContextInitializer : DbContextTypesInfoInitializerBase {
//	protected override DbContext CreateDbContext() {
//		var optionsBuilder = new DbContextOptionsBuilder<XAFEFCoreDbContext>()
//            .UseSqlServer(";")
//            .UseChangeTrackingProxies()
//            .UseObjectSpaceLinkProxies();
//        return new XAFEFCoreDbContext(optionsBuilder.Options);
//	}
//}
////This factory creates DbContext for design-time services. For example, it is required for database migration.
//public class XAFDesignTimeDbContextFactory : IDesignTimeDbContextFactory<XAFEFCoreDbContext> {
//	public XAFEFCoreDbContext CreateDbContext(string[] args) {
//		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
//		//var optionsBuilder = new DbContextOptionsBuilder<XAFEFCoreDbContext>();
//		//optionsBuilder.UseSqlServer("Integrated Security=SSPI;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=GigaApp.XAF");
//        //optionsBuilder.UseChangeTrackingProxies();
//        //optionsBuilder.UseObjectSpaceLinkProxies();
//		//return new XAFEFCoreDbContext(optionsBuilder.Options);
//	}
//}
//[TypesInfoInitializer(typeof(XAFContextInitializer))]
//public class XAFEFCoreDbContext : DbContext {
//	public XAFEFCoreDbContext(DbContextOptions<XAFEFCoreDbContext> options) : base(options) {
//	}
//	//public DbSet<ModuleInfo> ModulesInfo { get; set; }
//	public DbSet<FileData> FileData { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder) {
//        base.OnModelCreating(modelBuilder);
//        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
//        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
//    }
//}
