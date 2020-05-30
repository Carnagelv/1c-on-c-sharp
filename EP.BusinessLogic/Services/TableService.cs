using OneC.EntityData.Context;

namespace OneC.BusinessLogic.Services
{
    public interface ITableService : IService<Table>
    {
    }

    public class TableService : BaseService<Table>, ITableService
    {
        public TableService(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}
