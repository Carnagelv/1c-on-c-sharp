using OneC.EntityData.Context;

namespace OneC.BusinessLogic.Services
{
    public interface ITableRowItemService : IService<TableRowItem>
    {
    }

    public class TableRowItemService : BaseService<TableRowItem>, ITableRowItemService
    {
        public TableRowItemService(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}
