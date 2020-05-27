using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OneC.EntityData.Context
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
      
        DbSet<TableColumn> TableColumns { get; set; }
    }

    public partial class DataContext : DbContext, IDataContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer<DataContext>(null);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }
        
        public DbSet<TableColumn> TableColumns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
