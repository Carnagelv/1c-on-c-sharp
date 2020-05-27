using OneC.BusinessLogic.Services;

namespace OneC.BusinessLogic.Managers
{
    public class TableItemManager : ITableItemManager
    {
        private readonly ITableItemService _tableItemService;

        public TableItemManager(ITableItemService tableItemService)
        {
            _tableItemService = tableItemService;
        }

    }

    public interface ITableItemManager
    {
    }
}
