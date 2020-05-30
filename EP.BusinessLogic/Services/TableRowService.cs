using OneC.EntityData.Context;

namespace OneC.BusinessLogic.Services
{
    public interface ITableRowService : IService<TableRow>
    {
    }

    public class TableRowService : BaseService<TableRow>, ITableRowService
    {
        public TableRowService(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}
