using OneC.BusinessLogic.Services;

namespace OneC.BusinessLogic.Managers
{
    public class TableColumnManager : ITableColumnManager
    {
        private readonly ITableColumnService _tableColumnService;

        public TableColumnManager(ITableColumnService tableColumnService)
        {
            _tableColumnService = tableColumnService;
        }

    }

    public interface ITableColumnManager
    {
    }
}
