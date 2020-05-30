using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OneC.EntityData.Context
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();

        DbSet<Table> Tables { get; set; }
        DbSet<TableColumn> TableColumns { get; set; }
        DbSet<TableRow> TableRows { get; set; }
        DbSet<TableRowItem> TableRowItems { get; set; }
    }

    public partial class DataContext : DbContext, IDataContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer<DataContext>(null);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<TableColumn> TableColumns { get; set; }
        public DbSet<TableRow> TableRows { get; set; }
        public DbSet<TableRowItem> TableRowItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableColumn>()
                .HasRequired(m => m.Table)
                .WithMany(m => m.TableColumns)
                .HasForeignKey(m => m.TableId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TableRow>()
                .HasRequired(m => m.TableColumn)
                .WithMany(m => m.TableRows)
                .HasForeignKey(m => m.TableColumnId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TableRowItem>()
               .HasRequired(m => m.TableRow)
               .WithMany(m => m.TableRowItems)
               .HasForeignKey(m => m.TableRowId)
               .WillCascadeOnDelete(true);
        }
    }
}
