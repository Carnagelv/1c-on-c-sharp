using OneC.EntityData.Context;

namespace OneC.BusinessLogic.Services
{
    public interface ITableItemService : IService<TableItem>
    {
    }

    public class TableItemService : BaseService<TableItem>, ITableItemService
    {
        public TableItemService(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}
