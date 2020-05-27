using AutoMapper;

namespace OneC.BusinessLogic.Models
{
    public class Mappings
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                //cfg.CreateMap<UserProfile, UserViewModel>()
                //    .ForMember(x => x.CreateDate, y => y.MapFrom(c => c.CreateDate.ToShortDateString()));

            });

            return config.CreateMapper();
        }
    }
}