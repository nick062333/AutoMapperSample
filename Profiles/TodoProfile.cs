using AutoMapper;
using AutoMapperSample.Models;

namespace AutoMapperSample.Profiles
{
    public class TodoProfile : Profile
    {
        protected TodoProfile()
        {
            //TSource -> TDestination
            CreateMap<Todo, TodoItemDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(s => s.Id))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.IsComplete, x => x.MapFrom(s => s.IsComplete))
                .ForMember(x => x.EditTime, x => x.MapFrom(s => s.EditTime))
                .ForMember(x => x.CreateTime, x => x.Ignore())
                .BeforeMap((src, dest) => {
                    //Mapping 之前執行...
                })
                .AfterMap((src, dest) => {
                    //Mapping 完成之後執行...
                })
                .ReverseMap();
        }
    }
}
