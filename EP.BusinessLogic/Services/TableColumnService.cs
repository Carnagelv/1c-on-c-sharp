using OneC.EntityData.Context;
using System.Collections.Generic;
using System.Linq;

namespace OneC.BusinessLogic.Services
{
    public interface ITableColumnService : IService<TableColumn>
    {
        bool SaveCatalog(string name);
        List<TableColumn> GetColumns();
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

            Add(new TableColumn
            {
                Name = name,
                ParentId = null
            });

            return true;
        }

        public List<TableColumn> GetColumns()
        {
            return dbSet.ToList();
        }
    }
}
