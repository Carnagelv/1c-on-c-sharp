using OneC.EntityData.Context;

namespace OneC.BusinessLogic.Services
{
    public interface ITableColumnService : IService<TableColumn>
    {
    }

    public class TableColumnService : BaseService<TableColumn>, ITableColumnService
    {
        public TableColumnService(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}
