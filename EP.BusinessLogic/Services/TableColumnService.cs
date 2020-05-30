using OneC.EntityData.Context;
using System.Collections.Generic;
using System.Linq;

namespace OneC.BusinessLogic.Services
{
    public interface ITableColumnService : IService<TableColumn>
    {
        bool SaveCatalog(string name);
    }

    public class TableColumnService : BaseService<TableColumn>, ITableColumnService
    {
        public TableColumnService(IDataContext dataContext) : base(dataContext)
        {
        }

        public bool SaveCatalog(string name)
        {
            if (dbSet.Any(a => a.Name == name))
                return false;

            var table = new Table
            {
                InitialColumnName = name
            };

            table.TableColumns.Add(new TableColumn
            {
                Name = name,
                ParentId = null,
                IsInitial = true
            });

            dataContext.Tables.Add(table);
            dataContext.SaveChanges();

            return true;
        }
    }
}
