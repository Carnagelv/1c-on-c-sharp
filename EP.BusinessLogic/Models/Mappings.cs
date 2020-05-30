using AutoMapper;
using OneC.EntityData.Context;
using OneC.ViewModels;

namespace OneC.BusinessLogic.Models
{
    public class Mappings
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TableColumn, ColumnVieModel>();
            });

            return config.CreateMapper();
        }
    }
}